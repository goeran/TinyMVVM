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
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;

namespace TinyMVVM.VSIntegration.Tests.Internal.Conventions
{
	public class UnitTestConventionSpecs
	{
		[TestFixture]
		public class When_apply_Convention : UnitTestConventiontTestScenario
		{
			[SetUp]
			public void Setup()
			{
				Given(VisualStudio_Solution_exists);
				And(Solution_has_a_UnitTest_Project);
				And("ViewModel is described", () =>
				{
					mvvmDefinition = new ModelSpecification();
					mvvmDefinition.AddViewModel(new ViewModel("Login"));
				});
				And(UnitTestConvention_is_created);

				When("apply Convention", () =>
					convention.Apply(mvvmDefinition, mvvmFile));
			}

			[Test]
			public void assure_ViewModel_folder_been_added_to_unit_test_project()
			{
				Then(() =>
				{
					var testProject = solution.Projects.Last();
					testProject.HasFolder("ViewModel").ShouldBeTrue();
				});
			}

			[Test]
			public void assure_TestScenarios_Folder_has_been_added_to_folder()
			{
				Then(() =>
				{
					var folder = solution.Projects.Last().GetSubFolder("ViewModel");
					folder.HasFolder("TestScenarios").ShouldBeTrue();
				});
			}

			[Test]
			public void assure_TestScenario_files_has_been_added_to_TestScenarios_folder()
			{
				Then(() =>
				{
					var folder = solution.Projects.Last().GetSubFolder("ViewModel").GetSubFolder("TestScenarios");

					for (int i = 0; i < mvvmDefinition.ViewModels.Count; i++)
					{
						var fileName = string.Format("{0}TestScenario.cs", mvvmDefinition.ViewModels.ElementAt(i).Name);

						folder.HasFile(fileName).ShouldBeTrue();
					}
				});
			}

			[Test]
			public void assure_code_for_TestScenarios_has_been_generated()
			{
				Then(() =>
				{
					var folder = solution.Projects.Last().GetSubFolder("ViewModel").GetSubFolder("TestScenarios");

					for (int i = 0; i < mvvmDefinition.ViewModels.Count; i++)
					{
						var fileName = string.Format("{0}TestScenario.cs", mvvmDefinition.ViewModels.ElementAt(i).Name);

						codeGenServiceFake.Verify(s => s.Generate(mvvmFile, folder.GetFile(fileName), It.IsAny<CodeGeneratorArgs>()), Times.Once());
					}
				});
			}

			[Test]
			public void assure_UnitTest_files_has_been_added_to_folder()
			{
				Then(() =>
				{
					var folder = solution.Projects.Last().GetSubFolder("ViewModel");

					for (int i = 0; i < mvvmDefinition.ViewModels.Count; i++)
					{
						var fileName = string.Format("{0}Tests.cs", mvvmDefinition.ViewModels.ElementAt(i).Name);

						folder.HasFile(fileName).ShouldBeTrue();
					}
				});
			}

			[Test]
			public void assure_code_for_UnitTest_files_has_been_generated()
			{
				Then(() =>
				{
					var folder = solution.Projects.Last().GetSubFolder("ViewModel");

					for (int i = 0; i < mvvmDefinition.ViewModels.Count; i++)
					{
						var fileName = string.Format("{0}Tests.cs", mvvmDefinition.ViewModels.ElementAt(i).Name);

						codeGenServiceFake.Verify(s => s.Generate(mvvmFile, folder.GetFile(fileName), It.IsAny<CodeGeneratorArgs>()), Times.Once());
					}
				});
			}
		}

		[TestFixture]
		public class When_apply_Convention_and_MvvmFile_is_in_a_folder_deep_in_the_Project_tree : UnitTestConventiontTestScenario
		{
			[SetUp]
			public void Setup()
			{
				Given(VisualStudio_Solution_exists);
				And(Solution_has_a_UnitTest_Project);
				And("Project has a .mvvm file deep in the tree", () =>
				{
					mvvmFile = project.NewFolder("Test").NewFolder("SubFolder").NewFolder("ViewModel").NewFile("viewmodel.mvvm");
				});
				And("ViewModel is described", () =>
				{
					mvvmDefinition = new ModelSpecification();
 					mvvmDefinition.AddViewModel(new ViewModel("EditUserViewModel"));
				});
				And(UnitTestConvention_is_created);

				When("apply Convention", () =>
					convention.Apply(mvvmDefinition, mvvmFile));
			}

			[Test]
			public void assure_Folder_tree_for_the_mvvmFile_replicated_in_the_UnitTest_project()
			{
				Then(() =>
				{
					var unitTestProject = solution.Projects.Last();
					unitTestProject.GetSubFolder("Test").GetSubFolder("SubFolder").GetSubFolder("ViewModel").ShouldNotBeNull();
				});
			}
		}

		[TestFixture]
		public class When_apply_Convention_and_MvvmFile_is_in_project_root : UnitTestConventiontTestScenario
		{
			[SetUp]
			public void Setup()
			{
				Given(VisualStudio_Solution_exists);
				And(Solution_has_a_UnitTest_Project);
				And(".mvvm file is in Project root", () =>
				{
					mvvmFile = project.NewFile("viewmodel.mvvm");
				});
				And("ViewModel is described", () =>
				{
					mvvmDefinition = new ModelSpecification();
					mvvmDefinition.AddViewModel(new ViewModel("Login"));
				});
				And(UnitTestConvention_is_created);

				When("apply Convention", () =>
					convention.Apply(mvvmDefinition, mvvmFile));
			}

			[Test]
			public void assure_no_sub_Folders_are_created_in_UnitTestProject()
			{
				Then(() =>
				{
					//It has one folder (ViewModel), because the production code project has
					//a .mvvm file in the 'ViewModel' sub folder.
					project.Folders.Count().ShouldBe(1);
				});
			}
		}

		public class UnitTestConventiontTestScenario : NUnitScenarioClass
		{
			protected static UnitTestsConvention convention;
			protected static Solution solution;
			protected static Project project;
			protected static File mvvmFile;
			protected static ModelSpecification mvvmDefinition;
			protected static Mock<ICodeGeneratorService> codeGenServiceFake;

			protected Context VisualStudio_Solution_exists = () =>
			{
				solution = FakeData.VisualStudioSolution;
				project = solution.Projects.First();
				mvvmFile = solution.Projects.First().GetSubFolder("ViewModel").GetFile("viewmodel.mvvm");
			};

			protected Context Solution_has_a_UnitTest_Project = () =>
			{
				solution.NewProject(solution.Name + ".Tests");
			};


			protected Context UnitTestConvention_is_created = () =>
			{
				codeGenServiceFake = new Mock<ICodeGeneratorService>();
				convention = new UnitTestsConvention(codeGenServiceFake.Object, new ModelFactory());
			};

		}
	}
}
