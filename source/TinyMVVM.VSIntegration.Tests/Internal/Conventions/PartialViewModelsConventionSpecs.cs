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
                And("ViewModel is described", () =>
                {
                    mvvmDefinition = new ModelSpecification();
                    var loginViewModel = new ViewModel("Login");
                    loginViewModel.AddProperty(new ViewModelProperty("Username", "string", false));
                    loginViewModel.AddProperty(new ViewModelProperty("Password", "string", false));
                    loginViewModel.AddCommand(new ViewModelCommand("Login"));
                    mvvmDefinition.AddViewModel(loginViewModel);
                });

                When("apply convention", () =>
                    convention.ApplyToProject(mvvmDefinition, project));
            }

            [Test]
            public void assure_Partial_classes_for_ViewModels_are_created()
            {
                Then(() =>
                {
                    var viewModelFolder = project.GetSubFolder("ViewModel");
                    viewModelFolder.Files.Where(f => f.Name == "Login.cs").Count().ShouldBe(1);
                });
            }
        }

        public class TestScenario : NUnitScenarioClass
        {
            protected static PartialViewModelsConvention convention;
            protected static Solution solution;
            protected static Project project;
            protected static ModelSpecification mvvmDefinition;

            protected Context Convention_is_created = () =>
            {
                convention = new PartialViewModelsConvention();
            };

            protected Context VisualStudio_Solution_exists = () =>
            {
                solution = new Solution();
                solution.Name = "Rich RememberTheMilk";
                var rtmProject = new Project();
                rtmProject.Name = "RichRemembertheMilk";
                rtmProject.NewFolder("ViewModel");
                solution.Projects.Add(rtmProject);

                project = rtmProject;
            };
        }
    }
}
