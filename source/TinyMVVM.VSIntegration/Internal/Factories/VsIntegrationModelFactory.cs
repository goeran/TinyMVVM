using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;
using File = TinyMVVM.TinyMVVM_VSIntegration.Internal.Model.File;
using Project = TinyMVVM.TinyMVVM_VSIntegration.Internal.Model.Project;
using ProjectItem = EnvDTE.ProjectItem;
using Solution = TinyMVVM.TinyMVVM_VSIntegration.Internal.Model.Solution;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Factories
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

				if (vsProject.FullName != null && vsProject.FullName != string.Empty)
				{
					var project = new ProjectProxy(vsProject.Name);
					project.VsProject = vsProject;
					project.DirectoryPath = new System.IO.FileInfo(vsProject.FullName).Directory.FullName;
					solution.Projects.Add(project);

					for (int x = 1; x <= vsProject.ProjectItems.Count; x++)
					{
						var item = vsProject.ProjectItems.Item(x);
						if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
						{
							AddSubFolderInProject(project, item);
						}
					}
				}
            }

            return solution;
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
                    var newFile = new FileProxy(parentFolder);
                    newFile.VsProjectItem = vsItem;
                    newFile.Name = vsItem.Name;
                	newFile.Parent = parentFolder;
					newFile.DirectoryPath = new System.IO.FileInfo(vsItem.get_FileNames(0)).Directory.FullName;


                    parentFolder.Items.Add(newFile);
                }
            }
        }
    }

    public class SolutionProxy : Solution
    {
        public EnvDTE.Solution VsSolution { get; set; }
    
		internal SolutionProxy(string path) : base(path)
		{
			
		}
	}

    public class ProjectProxy : Project 
    {
        public EnvDTE.Project VsProject { get; set; }

		internal ProjectProxy(string name) : base(name)
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

    public class FileProxy : File
    {
        public EnvDTE.ProjectItem VsProjectItem { get; set; }

		internal FileProxy(Folder parentFolder) : base(parentFolder)
		{
			
		}

		public override void NewCodeBehindFile(string name)
		{
			base.NewCodeBehindFile(name);

			var file = CodeBehindFiles.Last();
			using (var f = file.NewFileStream()) ;
			VsProjectItem.ProjectItems.AddFromFile(file.Path);
		}
    }
}
