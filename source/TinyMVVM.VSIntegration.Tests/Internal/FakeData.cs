using System;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model;
using IO = System.IO;

namespace TinyMVVM.VSIntegration.Tests.Internal
{
    public class FakeData
    {
        public static Solution VisualStudioSolution
        {
            get
            {
                var modelFactory = new ModelFactory();

                //File paths
                var visualStudioDir = IO.Path.Combine(Environment.CurrentDirectory, "VisualStudioSolution");
                var vsSolutionPath = IO.Path.Combine(visualStudioDir, "RichRememberTheMilk.sln");
                var projectDir = IO.Path.Combine(visualStudioDir, "RichRememberTheMilk");
                var projectPath = IO.Path.Combine(projectDir, "RichRememberTheMilk.proj");
                var viewModelFolderDir = IO.Path.Combine(projectDir, "ViewModel");
                var viewModelFilePath = IO.Path.Combine(viewModelFolderDir, "viewmodel.mvvm");

                //Create folder and files
                IO.Directory.CreateDirectory(visualStudioDir);
                IO.Directory.CreateDirectory(projectDir);
                IO.Directory.CreateDirectory(viewModelFolderDir);
                using (var f = IO.File.Create(vsSolutionPath)) ;
                using (var f = IO.File.Create(projectPath)) ;
                using (var f = IO.File.Create(viewModelFilePath)) ;

                var solution = modelFactory.NewSolution(vsSolutionPath);
                solution.Name = "RichRememberTheMilk";

                var rtmProject = solution.NewProject("RichRememberTheMilk");
                rtmProject.DirectoryPath = projectDir;
                var mvvmFile = rtmProject.NewFolder("ViewModel").NewFile("viewmodel.mvvm");
                mvvmFile.DirectoryPath = viewModelFolderDir;
                solution.Projects.Add(rtmProject);

                return solution;
            }
        }
    }
}
