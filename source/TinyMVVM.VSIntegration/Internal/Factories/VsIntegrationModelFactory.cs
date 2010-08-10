using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using File = TinyMVVM.VSIntegration.Internal.Model.VsSolution.File;
using Project = TinyMVVM.VSIntegration.Internal.Model.VsSolution.Project;
using ProjectItem = EnvDTE.ProjectItem;
using Solution = TinyMVVM.VSIntegration.Internal.Model.VsSolution.Solution;

namespace TinyMVVM.VSIntegration.Internal.Factories
{
    internal class VsIntegrationModelFactory : IModelFactory
    {
        private DTE dte;

        public VsIntegrationModelFactory(EnvDTE.DTE dte)
        {
            this.dte = dte;
        }

        public Solution NewSolution(string path)
        {
            var solution = new SolutionProxy(path);
            solution.VsSolution = dte.Solution;
            solution.Path = dte.Solution.FullName;

            for (int i = 1; i <= dte.Solution.Projects.Count; i++)
            {
                var vsProject = dte.Solution.Projects.Item(i);

                if (IsVirtualFolder(vsProject.Kind))                 
                {
                    FindAndAddProjects(vsProject, solution);
                }
                else if (vsProject.FullName != null && vsProject.FullName != string.Empty)
				{
					AddProject(solution, vsProject);
				}
            }

            return solution;
        }

        private bool IsVirtualFolder(string kind)
        {
            return kind == EnvDTE80.ProjectKinds.vsProjectKindSolutionFolder;
        }

        private void FindAndAddProjects(EnvDTE.Project vsProject, SolutionProxy solution)
        {
            for (int x = 1; x <= vsProject.ProjectItems.Count; x++)
            {
                var item = vsProject.ProjectItems.Item(x);
                if (item.SubProject != null)
                {
                    if (IsVirtualFolder(item.SubProject.Kind))
                    {
                        FindAndAddProjects(item.SubProject, solution);
                    }
                    else 
                    {
                        AddProject(solution, item.SubProject);
                    }
                }
            }
        }

        private void AddProject(SolutionProxy solution, EnvDTE.Project vsProject)
        {
            var project = new ProjectProxy(vsProject.Name, solution);
            project.VsProject = vsProject;
            project.DirectoryPath = new System.IO.FileInfo(vsProject.FullName).Directory.FullName;
            project.RootNamespace = ParseRootNamespace(vsProject);
            solution.Projects.Add(project);

            for (int x = 1; x <= vsProject.ProjectItems.Count; x++)
            {
                var item = vsProject.ProjectItems.Item(x);
                if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    AddSubFolderInProject(project, item);
                }
                else if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                    AddFileToFolder(project, item);
                }
            }
        }

        private string ParseRootNamespace(EnvDTE.Project vsProject)
		{
			string result = null;

			foreach (EnvDTE.Property property in vsProject.Properties)
			{
				if (property.Name == "RootNamespace")
				{
					result = property.Value.ToString();
					break;
				}
			}

			return result;
		}

    	private File AddFileToFolder(Folder folder, ProjectItem item)
    	{
			var newFile = new FileProxy(folder);
    		newFile.VsProjectItem = item;
    		newFile.Name = item.Name;
    		folder.Items.Add(newFile);

			foreach (ProjectItem codeBehindItem in item.ProjectItems)
			{
				var codeBehindFile = new FileProxy(folder);
				codeBehindFile.Name = codeBehindItem.Name;
				codeBehindFile.VsProjectItem = codeBehindItem;
				newFile.Items.Add(codeBehindFile);
			}

    		return newFile;
    	}

    	private void AddSubFolderInProject(Project project, ProjectItem vsProjectItem)
        {
            var newFolder = new FolderProxy(vsProjectItem.Name, project);
            newFolder.VsProjectItem = vsProjectItem;
        	newFolder.DirectoryPath = new System.IO.DirectoryInfo(vsProjectItem.get_FileNames(0)).FullName;

            project.Items.Add(newFolder);
                    
            if (vsProjectItem.ProjectItems.Count > 0)
            {
                AddFilesAndFolders(newFolder, vsProjectItem);
            }
        }

        private void AddFilesAndFolders(Folder parentFolder, ProjectItem parentVsItem)
        {
            for (int x = 1; x <= parentVsItem.ProjectItems.Count; x++)
            {
                var vsItem = parentVsItem.ProjectItems.Item(x);
                if (vsItem.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    var newFolder = new FolderProxy(vsItem.Name, parentFolder);
                    newFolder.VsProjectItem = vsItem;
					newFolder.DirectoryPath = new System.IO.DirectoryInfo(vsItem.get_FileNames(0)).FullName;
            
                    parentFolder.Items.Add(newFolder);

                    if (vsItem.ProjectItems.Count > 0)
                        AddFilesAndFolders(newFolder, vsItem);
                }
                else if (vsItem.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                	var newFile = AddFileToFolder(parentFolder, vsItem);
                }
            }
        }

		public Folder NewFolder(string name, Folder parentFolder)
		{
			if (parentFolder is FolderProxy)
			{
				var parentFolderProxy = parentFolder as FolderProxy;
				var newFolder = new FolderProxy(name, parentFolderProxy);
				var newVsFolder = parentFolderProxy.VsProjectItem.ProjectItems.AddFolder(name, EnvDTE.Constants.vsProjectItemKindPhysicalFolder);
				newFolder.VsProjectItem = newVsFolder;
				return newFolder;
			}
			else
			{
				var parentProjectProxy = parentFolder as ProjectProxy;
				var newFolder = new FolderProxy(name, parentProjectProxy);
				var newVsFolder = parentProjectProxy.VsProject.ProjectItems.AddFolder(name, EnvDTE.Constants.vsProjectItemKindPhysicalFolder);
				newFolder.VsProjectItem = newVsFolder;
				return newFolder;				
			}
		}

		public File NewFile(string name, Folder parentFolder)
		{
			var parentFolderProxy = parentFolder as FolderProxy;

			var newFile = new FileProxy(parentFolder){ Name = name };

			using (var f = newFile.NewFileStream()) ;
			var newProjectItem = parentFolderProxy.VsProjectItem.ProjectItems.AddFromFile(newFile.Path);

			newFile.VsProjectItem = newProjectItem;

			return newFile;
		}
    }

    public class SolutionProxy : Model.VsSolution.Solution
    {
        public EnvDTE.Solution VsSolution { get; set; }
    
		internal SolutionProxy(string path) : base(path)
		{
			
		}
	}

    public class ProjectProxy : Model.VsSolution.Project 
    {
        public EnvDTE.Project VsProject { get; set; }

		internal ProjectProxy(string name, Solution solution) : base(name, solution)
		{
			
		}

        public override Folder NewFolder(string name)
        {
            var result = base.NewFolder(name);

            if (Directory.Exists(result.Path))
                VsProject.ProjectItems.AddFromDirectory(result.Path);
            else
                VsProject.ProjectItems.AddFolder(name, EnvDTE.Constants.vsProjectItemKindPhysicalFolder);

            return result;
        }

		public override File NewFile(string name)
		{
			var result = base.NewFile(name);

			using (var f = result.NewFileStream()) ;
			VsProject.ProjectItems.AddFromFile(result.Path);

			return result;
		}
    }

    public class FolderProxy : Folder
    {
        public EnvDTE.ProjectItem VsProjectItem { get; set; }

		internal FolderProxy(string name, Folder parentFolder) : base(name, parentFolder)
		{
			
		}

        public override Folder NewFolder(string name)
        {
            var result = base.NewFolder(name);


			if (Directory.Exists(result.Path))
				VsProjectItem.ProjectItems.AddFromDirectory(result.Path);
			else
				VsProjectItem.ProjectItems.AddFolder(name, EnvDTE.Constants.vsProjectItemKindPhysicalFolder);

            return result;
        }

		public override File NewFile(string name)
		{
			var result = base.NewFile(name);

			using (var f = result.NewFileStream()) ;
			VsProjectItem.ProjectItems.AddFromFile(result.Path);

			return result;
		}
    }

    public class FileProxy : Model.VsSolution.File
    {
        public EnvDTE.ProjectItem VsProjectItem { get; set; }

		internal FileProxy(Folder parentFolder) : base(parentFolder)
		{
			
		}

		public override File NewCodeBehindFile(string name)
		{
			var file = base.NewCodeBehindFile(name);

			using (var f = file.NewFileStream()) ;
			VsProjectItem.ProjectItems.AddFromFile(file.Path);

			return file;
		}

		public override File DeleteCodeBehindFile(string name)
		{
			var file = base.DeleteCodeBehindFile(name);

			foreach (ProjectItem item in VsProjectItem.ProjectItems)
			{
				if (name == item.Name)
					item.Delete();
			}

			return file;
		}

		public override void DeleteAllCodeBehindFiles()
		{
			base.DeleteAllCodeBehindFiles();

			foreach (ProjectItem item in VsProjectItem.ProjectItems)
			{
				item.Delete();
			}
		}
    }
}
