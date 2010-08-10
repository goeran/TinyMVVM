using System;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
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
                var slProjectDir = IO.Path.Combine(visualStudioDir, "RichRememberTheMilk.SL");
                var slProjectPath = IO.Path.Combine(slProjectDir, "RichRememberTheMilk.proj");
                var integrationTestsProjectDir = IO.Path.Combine(visualStudioDir, "RichRememberTheMilk.IntegrationTests");
                var integrationTestsProjectPath = IO.Path.Combine(integrationTestsProjectDir, "RichRememberTheMilk.IntegrationTests.proj");

                //Create folder and files
                IO.Directory.CreateDirectory(visualStudioDir);
                IO.Directory.CreateDirectory(projectDir);
                IO.Directory.CreateDirectory(viewModelFolderDir);
                IO.Directory.CreateDirectory(slProjectDir);
                IO.Directory.CreateDirectory(integrationTestsProjectDir);
                using (var f = IO.File.Create(vsSolutionPath)) ;
                using (var f = IO.File.Create(projectPath)) ;
                using (var f = IO.File.Create(viewModelFilePath)) ;
                using (var f = IO.File.Create(slProjectPath)) ;
                using (var f = IO.File.Create(integrationTestsProjectPath)) ;

                var solution = modelFactory.NewSolution(vsSolutionPath);
                solution.Name = "RichRememberTheMilk";

                var rtmProject = solution.NewProject("RichRememberTheMilk");
                rtmProject.DirectoryPath = projectDir;
                var mvvmFile = rtmProject.NewFolder("ViewModel").NewFile("viewmodel.mvvm");
                mvvmFile.DirectoryPath = viewModelFolderDir;

                var rtmSLProject = solution.NewProject("RichRememberTheMilk.SL");
                rtmSLProject.DirectoryPath = slProjectDir;

                var integrationTestsProject = solution.NewProject("RichRememberTheMilk.IntegrationTests");
                integrationTestsProject.DirectoryPath = integrationTestsProjectDir;

                return solution;
            }
        }
    }
}
