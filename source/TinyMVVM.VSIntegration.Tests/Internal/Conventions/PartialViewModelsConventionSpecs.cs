using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.Tests;
using TinyMVVM.VSIntegration.Internal.Conventions;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;

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
            public void assure_files_for_partial_ViewModel_classes_are_added_to_the_dir_where_the_mvvmFile_is_located()
            {
                Then(() =>
                {
                    var viewModelFolder = project.GetSubFolder("ViewModel");
                    viewModelFolder.HasFile("Login.cs").ShouldBeTrue();
					viewModelFolder.HasFile("MainScreen.cs").ShouldBeTrue();
                });
            }

        	[Test]
        	public void assure_code_for_partial_classes_are_generated()
        	{
        		Then(() =>
        		{
        			var viewModelFolder = project.GetSubFolder("ViewModel");
        			for (int i = 1; i < viewModelFolder.Files.Count(); i++)
        			{
						codeGenServiceFake.Verify(s => s.Generate(mvvmFile, viewModelFolder.Files.ElementAt(i), It.IsAny<CodeGeneratorArgs>()));        				
        			}
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
            public void assure_more_partial_class_files_are_not_added()
            {
                Then(() =>
                {
                    var viewModelFolder = project.GetSubFolder("ViewModel");
                    viewModelFolder.Files.Count().ShouldBe(mvvmDefinition.ViewModels.Count + 1);

                });
            }
        }

        public class TestScenario : NUnitScenarioClass
        {
        	protected static Mock<ICodeGeneratorService> codeGenServiceFake;
            protected static PartialViewModelsConvention convention;
            protected static Solution solution;
            protected static Project project;
            protected static File mvvmFile;
            protected static ModelSpecification mvvmDefinition;

            protected Context Convention_is_created = () =>
            {
				codeGenServiceFake = new Mock<ICodeGeneratorService>();
                convention = new PartialViewModelsConvention(codeGenServiceFake.Object);
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
