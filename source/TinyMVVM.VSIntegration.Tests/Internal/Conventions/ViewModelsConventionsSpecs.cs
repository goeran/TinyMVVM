using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Conventions;
using TinyMVVM.VSIntegration.Internal.Model;
using IO = System.IO;

namespace TinyMVVM.VSIntegration.Tests.Internal.Conventions
{
    public class ViewModelsConventionsSpecs
    {
        [TestFixture]
        public class When_Convention_is_applied : ViewModelConventionsTestScenario
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
            public void Then_assure_code_behind_files_for_ViewModels_are_created()
            {
                mvvmFile.CodeBehindFiles.Count().ShouldBe(modelSpecification.ViewModels.Count);
            }
        }

        [TestFixture]
        public class When_Convention_is_applied_for_the_nth_time : ViewModelConventionsTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given.VisualStudio_solution_exists();
                And.ViewModel_is_described();
                And.ViewModelsConvention_is_created();
                And.Convention_is_already_applied();

                When.Convention_is_applied();
            }

            [Test]
            public void Then_assure_created_code_behind_files_for_ViewModels_are_not_accumulated()
            {
                mvvmFile.CodeBehindFiles.Count().ShouldBe(modelSpecification.ViewModels.Count);
            }
        }

        public class ViewModelConventionsTestScenario : ScenarioClass<ViewModelConventionsTestScenario>
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
                solution = FakeData.VisualStudioSolution;

                project = solution.Projects.First();
                mvvmFile = solution.Projects.First().GetSubFolder("ViewModel").GetFile("viewmodel.mvvm");
            }

            public void Convention_is_already_applied()
            {
                convention.Apply(modelSpecification, mvvmFile);
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
