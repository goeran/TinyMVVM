using System;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;
using IO = System.IO;

namespace TinyMVVM.VSIntegration.Tests.Internal
{
    public class FakeData
    {
        public static Solution VisualStudioSolution
        {
            get
            {
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

                var solution = new Solution();
                solution.Name = "RichRememberTheMilk";
                solution.Path = vsSolutionPath;

                var rtmProject = new Project();
                rtmProject.Path = projectPath;
                rtmProject.Name = "RichRemembertheMilk";
                var mvvmFile = rtmProject.NewFolder("ViewModel").NewFile("viewmodel.mvvm");
                mvvmFile.Path = viewModelFilePath;
                solution.Projects.Add(rtmProject);

                return solution;
            }
        }
    }
}
