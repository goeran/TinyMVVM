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
	public class ControllersConventionSpec
	{
		[TestFixture]
		public class When_apply_Convention : ControllersConventionTestScenario
		{
			[SetUp]
			public void Setup()
			{
				Given(VisualStudioSolution_exists);
				And("ViewModel is described", () =>
				{
					mvvmSpecification = new ModelSpecification();
					mvvmSpecification.AddViewModel(new ViewModel("Login"));
				});
				And(ControllersConvention_is_created);

				When("apply Convention", () =>
					convention.Apply(mvvmSpecification, mvvmFile));
			}

			[Test]
			public void assure_Controllers_folder_is_added_to_Project()
			{
				Then(() =>
				     mvvmFile.Project.HasFolder("Controllers").ShouldBeTrue());
			}

			[Test]
			public void assure_Controllers_are_added_to_Controllers_folder()
			{
				Then(() =>
				{
					var controllersFolder = mvvmFile.Project.GetSubFolder("Controllers");

					foreach (var viewModel in mvvmSpecification.ViewModels)
					{
						controllersFolder.HasFile(viewModel.Name + "Controller.cs").ShouldBeTrue();
					}
				});
			}

			[Test]
			public void assure_code_for_each_Controller_is_generated()
			{
				Then(() =>
				{
					var controllersFolder = mvvmFile.Project.GetSubFolder("Controllers");

					foreach (var viewModel in mvvmSpecification.ViewModels)
					{
						var fileName = string.Format("{0}Controller.cs", viewModel.Name);

						codeGenServiceFake.Verify(s => s.Generate(mvvmFile, controllersFolder.GetFile(fileName), It.IsAny<CodeGeneratorArgs>()), Times.Once());
					}
				});
			}
		}

		[TestFixture]
		public class When_apply_Convention_for_nth_time : ControllersConventionTestScenario
		{
			[SetUp]
			public void Setup()
			{
				Given(VisualStudioSolution_exists);
				And("ViewModel is described", () =>
				{
					mvvmSpecification = new ModelSpecification();
					mvvmSpecification.AddViewModel(new ViewModel("Login"));
				});
				And(ControllersConvention_is_created);
				And("Convention is already applied", () =>
					convention.Apply(mvvmSpecification, mvvmFile));

				When("apply Convention for second time", () =>
					convention.Apply(mvvmSpecification, mvvmFile));
			}

			[Test]
			public void assure_there_is_only_one_Controllers_folder_in_Project()
			{
				Then(() =>
				     mvvmFile.Project.Folders.Where(f => f.Name == "Controllers").Count().ShouldBe(1));
			}

			[Test]
			public void assure_there_are_only_one_Controller_file_in_folder_for_each_ViewModel()
			{
				Then(() =>
				{
					var controllersFolder = mvvmFile.Project.GetSubFolder("Controllers");

					foreach (var viewModel in mvvmSpecification.ViewModels)
					{
						var fileName = string.Format("{0}Controller.cs", viewModel.Name);
						controllersFolder.Files.Where(f => f.Name == fileName).Count().ShouldBe(1);
					}
				});
			}

			[Test]
			public void assure_existing_files_is_not_overwritten()
			{
				Then(() =>
				{
					var controllersFolder = mvvmFile.Project.GetSubFolder("Controllers");

					foreach (var viewModel in mvvmSpecification.ViewModels)
					{
						var fileName = string.Format("{0}Controller.cs", viewModel.Name);

						codeGenServiceFake.Verify(s => s.Generate(mvvmFile, controllersFolder.GetFile(fileName), It.IsAny<CodeGeneratorArgs>()), Times.Once());
					}
				});
			}
		}

		public class ControllersConventionTestScenario : NUnitScenarioClass
		{
			protected static ControllersConvention convention;
			protected static File mvvmFile;
			protected static ModelSpecification mvvmSpecification;
			protected static Mock<ICodeGeneratorService> codeGenServiceFake;

			protected Context VisualStudioSolution_exists = () =>
			{
				mvvmFile = FakeData.VisualStudioSolution.Projects.First().GetSubFolder("ViewModel").GetFile("viewmodel.mvvm");
			};

			protected Context ControllersConvention_is_created = () =>
			{
				codeGenServiceFake = new Mock<ICodeGeneratorService>();
				convention = new ControllersConvention(codeGenServiceFake.Object, new ModelFactory());
			};
		}
	}
}
