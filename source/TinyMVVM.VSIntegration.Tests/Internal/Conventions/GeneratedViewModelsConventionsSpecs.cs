using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Conventions;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;
using IO = System.IO;

namespace TinyMVVM.VSIntegration.Tests.Internal.Conventions
{
    public class GeneratedViewModelsConventionsSpecs
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
            public void Then_assure_code_behind_files_added_to_mvvmFile()
            {
                mvvmFile.CodeBehindFiles.Count().ShouldBe(modelSpecification.ViewModels.Count);
            }

        	[Test]
        	public void Then_assure_code_for_code_behind_files_are_generated()
        	{
				codeGenServiceFake.Verify(s => s.Generate(mvvmFile, mvvmFile.CodeBehindFiles.ElementAt(0), It.IsAny<CodeGeneratorArgs>()));
				codeGenServiceFake.Verify(s => s.Generate(mvvmFile, mvvmFile.CodeBehindFiles.ElementAt(1), It.IsAny<CodeGeneratorArgs>()));
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
            public void Then_assure_added_code_behind_files_are_not_accumulated()
            {
                mvvmFile.CodeBehindFiles.Count().ShouldBe(modelSpecification.ViewModels.Count);
            }
        }

        public class ViewModelConventionsTestScenario : ScenarioClass<ViewModelConventionsTestScenario>
        {
            private GeneratedViewModelsConvention convention;
        	protected Mock<ICodeGeneratorService> codeGenServiceFake;

            public void ViewModelsConvention_is_created()
            {
            	codeGenServiceFake = new Mock<ICodeGeneratorService>();
                convention = new GeneratedViewModelsConvention(codeGenServiceFake.Object);
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
