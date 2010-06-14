using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Tests.Internal.Conventions
{
    public class ViewModelsConventionsSpecs
    {
        [TestFixture]
        public class When_Convention_is_applied : ViewModelConventionsTestScenario<When_Convention_is_applied>
        {
            [SetUp]
            public void Setup()
            {
                Given.VisualStudio_solution_exists();
                And.ViewModel_is_described();
                And.ViewModelsConvention_is_created();

                When.Convention_is_applied();
            }

            [Test]
            public void Then_assure_ViewModels_are_created()
            {
                mvvmFile.CodeBehindFiles.Count().ShouldBe(modelSpecification.ViewModels.Count);
            }
        }

        public class ViewModelConventionsTestScenario<T> : ScenarioClass<T> where T: class
        {
            private ViewModelsConvention convention;

            public void ViewModelsConvention_is_created()
            {
                convention = new ViewModelsConvention();
            }

            protected Solution solution;
            protected File mvvmFile;
            protected Project project;

            public void VisualStudio_solution_exists()
            {
                solution = new Solution();
                solution.Name = "RichRememberTheMilk";
                var rtmProject = new Project();
                project = rtmProject;
                rtmProject.Name = "RichRemembertheMilk";
                mvvmFile = rtmProject.NewFolder("ViewModel").NewFile("viewmodel.mvvm");
                solution.Projects.Add(rtmProject);

            }

            protected ModelSpecification modelSpecification;

            public void ViewModel_is_described()
            {
                modelSpecification = new ModelSpecification();
                var loginViewModel = new ViewModel("Login");
                loginViewModel.AddProperty(new ViewModelProperty("Username", "string", false));
                loginViewModel.AddProperty(new ViewModelProperty("Password", "string", false));
                loginViewModel.AddCommand(new ViewModelCommand("Login"));
                modelSpecification.AddViewModel(loginViewModel);

                var mainScreenViewModel = new ViewModel("MainScreen");
                mainScreenViewModel.AddProperty(new ViewModelProperty("Title", "string", true));
                modelSpecification.AddViewModel(mainScreenViewModel);
            }

            public void Convention_is_applied()
            {
                convention.Apply(modelSpecification, mvvmFile);
            }
        }
    }
}
