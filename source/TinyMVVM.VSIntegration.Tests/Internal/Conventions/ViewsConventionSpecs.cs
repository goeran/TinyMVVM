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
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;

namespace TinyMVVM.VSIntegration.Tests.Internal.Conventions
{
    public class ViewsConventionSpecs
    {
        [TestFixture]
        public class When_Convention_is_applied_on_Project : ViewsConventionTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(ViewsConvention_is_created);
                And(Solution_is_created);
                And("ViewModel is described", () =>
                {
                    mvvmDefinition = new ModelSpecification();
                    var loginViewModel = new ViewModel("Login");
                    loginViewModel.AddProperty(new ViewModelProperty("Username", "String", false));
                    loginViewModel.AddProperty(new ViewModelProperty("Password", "String", false));
                    loginViewModel.AddCommand(new ViewModelCommand("Login"));
                    mvvmDefinition.AddViewModel(loginViewModel);
                });

                When(ApplyConvention);
            }

            [Test]
            public void assure_Views_folder_is_created()
            {
                Then(() =>
                {
                    var viewsFolder = project.Items.Where(i => i is Folder && i.Name == "Views").SingleOrDefault();
                    viewsFolder.ShouldNotBeNull();
                });
            }

            [Test]
            public void assure_Views_for_each_ViewModel_are_added_to_Views_Folder()
            {
                Then(() =>
                {
                    var viewsFolder = project.GetSubFolder("Views");
                    viewsFolder.Files.Count().ShouldBe(mvvmDefinition.ViewModels.Count);

                	foreach (var viewModel in mvvmDefinition.ViewModels)
                	{
                		var file = viewsFolder.GetFile(viewModel.Name + ".xaml");
						file.ShouldNotBeNull();
                		file.HasCodeBehindFile(viewModel.Name + ".xaml.cs").ShouldBeTrue();
                	}
					
                });
            }

        	[Test]
        	public void assure_code_for_each_View_is_generated()
        	{
        		Then(() =>
        		{
        			var viewsFolder = project.GetSubFolder("Views");

        			for (int i = 0; i < viewsFolder.Files.Count(); i++)
        			{
						codeGenServiceFake.Verify(s => s.Generate(mvvmFile, viewsFolder.Files.ElementAt(i), It.IsAny<CodeGeneratorArgs>()));
        			}
        		});
        	}

        	[Test]
        	public void assure_code_for_each_View_CodeBehind_file_is_generated()
        	{
				Then(() =>
				{
					var viewsFolder = project.GetSubFolder("Views");

					for (int i = 0; i < viewsFolder.Files.Count(); i++)
					{
						codeGenServiceFake.Verify(s => s.Generate(mvvmFile, viewsFolder.Files.ElementAt(i).CodeBehindFiles.ElementAt(0), It.IsAny<CodeGeneratorArgs>()));
					}
				});
        	}
        }

        [TestFixture]
        public class When_Convention_is_applied_on_Project_that_has_a_Views_folder : ViewsConventionTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(ViewsConvention_is_created);
                And(Solution_is_created);
                And("Project has 'Views' folder and Views", () =>
                {
                    var viewsFolder = project.NewFolder("Views");

                    viewsFolder.NewFile("Login.xaml");

                });
                And("ViewModel is described", () =>
                {
                    mvvmDefinition = new ModelSpecification();
                    var loginViewModel = new ViewModel("Login");
                    loginViewModel.AddProperty(new ViewModelProperty("Username", "String", false));
                    loginViewModel.AddProperty(new ViewModelProperty("Password", "String", false));
                    loginViewModel.AddCommand(new ViewModelCommand("Login"));
                    mvvmDefinition.AddViewModel(loginViewModel);
                });

                When(ApplyConvention);
            }

            [Test]
            public void assure_a_second_Views_folder_is_not_created()
            {
                Then(() =>
                {
                    project.Items.Where(i => i is Folder && i.Name == "Views").
                        Count().ShouldBe(1);
                });
            }

            [Test]
            public void assure_Views_for_each_ViewModel_are_not_duplicated_under_Views_folder()
            {
                Then(() =>
                {
                	var viewsFolder = project.GetSubFolder("Views");
                    viewsFolder.Files.Count().ShouldBe(mvvmDefinition.ViewModels.Count);

                    viewsFolder.Files.Where(f => f.Name == "Login.xaml").Count().ShouldBe(1);
                });
            }

        }
    }

    public class ViewsConventionTestScenario : NUnitScenarioClass
    {
    	protected static Mock<ICodeGeneratorService> codeGenServiceFake;
        protected static ViewsConvention viewsConvention;
        protected static Solution solution;
        protected static Project project;
        protected static File mvvmFile;
        protected static ModelSpecification mvvmDefinition;

        protected Context ViewsConvention_is_created = () =>
        {
			NewViewsConvention();
        };

    	private static void NewViewsConvention()
    	{
    		codeGenServiceFake = new Mock<ICodeGeneratorService>();
    		viewsConvention = new ViewsConvention(codeGenServiceFake.Object, new ModelFactory());
    	}

    	protected Context Solution_is_created = () =>
        {
            solution = FakeData.VisualStudioSolution;

            project = solution.Projects.First();
            mvvmFile = solution.Projects.First().GetSubFolder("ViewModel").GetFile("viewmodel.mvvm");
        };


        protected When ViewsConvention_is_spawned = () =>
        {
            NewViewsConvention();
        };

        protected When ApplyConvention = () =>
        {
            viewsConvention.Apply(mvvmDefinition, mvvmFile);
        };
    }
}
