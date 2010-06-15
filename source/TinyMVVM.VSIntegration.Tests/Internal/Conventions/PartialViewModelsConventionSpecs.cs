using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.Tests;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Tests.Internal.Conventions
{
    public class PartialViewModelsConventionSpecs
    {
        [TestFixture]
        public class When_Convention_is_applied_on_Project : TestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Convention_is_created);
                And(VisualStudio_Solution_exists);
                And(ViewModel_is_described);

                When("apply convention", () =>
                    convention.Apply(mvvmDefinition, mvvmFile));
            }

            [Test]
            public void assure_Partial_classes_for_ViewModels_are_created()
            {
                Then(() =>
                {
                    var viewModelFolder = project.GetSubFolder("ViewModel");
                    viewModelFolder.Files.Where(f => f.Name == "Login.cs").Count().ShouldBe(1);
                    viewModelFolder.Files.Where(f => f.Name == "MainScreen.cs").Count().ShouldBe(1);
                });
            }
        }

        [TestFixture]
        public class When_Convention_is_applied_on_a_Project_that_already_has_partial_classes_for_ViewModels : TestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Convention_is_created);
                And(VisualStudio_Solution_exists);
                And("Project already contains partial classes for ViewModel", () =>
                {
                    var viewModelFolder = project.GetSubFolder("ViewModel");
                    viewModelFolder.NewFile("Login.cs");
                    viewModelFolder.NewFile("MainScreen.cs");
                });
                And(ViewModel_is_described);

                When("apply convention", () =>
                    convention.Apply(mvvmDefinition, mvvmFile));
            }

            [Test]
            public void assure_more_partial_classes_are_not_added()
            {
                Then(() =>
                {
                    var viewModelFolder = project.GetSubFolder("ViewModel");
                    viewModelFolder.Files.Count().ShouldBe(3);

                });
            }
        }

        public class TestScenario : NUnitScenarioClass
        {
            protected static PartialViewModelsConvention convention;
            protected static Solution solution;
            protected static Project project;
            protected static File mvvmFile;
            protected static ModelSpecification mvvmDefinition;

            protected Context Convention_is_created = () =>
            {
                convention = new PartialViewModelsConvention();
            };

            protected Context VisualStudio_Solution_exists = () =>
            {
                solution = FakeData.VisualStudioSolution;

                project = solution.Projects.First();
                mvvmFile = solution.Projects.First().GetSubFolder("ViewModel").GetFile("viewmodel.mvvm");
            };

            protected Context ViewModel_is_described = () =>
            {
                mvvmDefinition = new ModelSpecification();
                var loginViewModel = new ViewModel("Login");
                loginViewModel.AddProperty(new ViewModelProperty("Username", "string", false));
                loginViewModel.AddProperty(new ViewModelProperty("Password", "string", false));
                loginViewModel.AddCommand(new ViewModelCommand("Login"));
                mvvmDefinition.AddViewModel(loginViewModel);

                var mainScreenViewModel = new ViewModel("MainScreen");
                mainScreenViewModel.AddProperty(new ViewModelProperty("Title", "string", true));
                mvvmDefinition.AddViewModel(mainScreenViewModel);
            };
        }
    }
}
