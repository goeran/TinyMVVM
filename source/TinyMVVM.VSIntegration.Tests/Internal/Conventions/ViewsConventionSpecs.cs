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
            public void assure_Views_for_each_ViewModel_are_created()
            {
                Then(() =>
                {
                    var viewsFolder = project.Folders.Where(n => n.Name == "Views").SingleOrDefault();
                    viewsFolder.Files.Count().ShouldBe(mvvmDefinition.ViewModels.Count);
                    
                    viewsFolder.Files.Where(f => f.Name == "Login.xaml").Count().ShouldBe(1);
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
                    var viewsFolder = new Folder() {Name = "Views"};
                    project.Items.Add(viewsFolder);

                    viewsFolder.Items.Add(new File() { Name = "Login.xaml"});

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
                        Count().ShouldNotBe(2);
                });
            }

            [Test]
            public void assure_Views_for_each_ViewModel_are_not_duplicated()
            {
                Then(() =>
                {
                    var viewsFolder = project.Folders.Where(n => n.Name == "Views").SingleOrDefault();
                    viewsFolder.Files.Count().ShouldBe(mvvmDefinition.ViewModels.Count);

                    viewsFolder.Files.Where(f => f.Name == "Login.xaml").Count().ShouldBe(1);
                });
            }

        }
    }

    public class ViewsConventionTestScenario : NUnitScenarioClass
    {
        protected static ViewsConvention viewsConvention;
        protected static Solution solution;
        protected static Project project;
        protected static File mvvmFile;
        protected static ModelSpecification mvvmDefinition;

        protected Context ViewsConvention_is_created = () =>
        {
            viewsConvention = new ViewsConvention();
        };

        protected Context Solution_is_created = () =>
        {
            solution = new Solution();
            solution.Name = "Rich RememberTheMilk";
            var rtmProject = new Project();
            rtmProject.Name = "RichRememberTheMilk";
            mvvmFile = rtmProject.NewFolder("ViewModel").NewFile("viewmodel.mvvm");
            solution.Projects.Add(rtmProject);

            project = rtmProject;
        };


        protected When ViewsConvention_is_spawned = () =>
        {
            viewsConvention = new ViewsConvention();
        };

        protected When ApplyConvention = () =>
        {
            viewsConvention.Apply(mvvmDefinition, mvvmFile);
        };
    }
}
