using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model
{
    public class ProjectSpecs
    {
        [TestFixture]
        public class When_spawned : ModelTestScenario
        {
            [SetUp]
            public void Setup()
            {
                When(Project_is_spawned);
            }

            [Test]
            public void assure_it_has_a_Name()
            {
                Then(() => project.Name.ShouldBeNull());
            }

            [Test]
            public void assure_it_has_Items()
            {
                Then(() => project.Items.ShouldNotBeNull());
            }

            [Test]
            public void assure_it_has_a_Type()
            {
                Then(() => project.Type.ShouldBeNull());
            }
        }

        [TestFixture]
        public class When_get_all_Folders_in_Project : ModelTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Project_is_created);
                And("Project contains 0 items");

                When("get all Folders", () =>
                    folders = project.Folders);
            }

            [Test]
            public void assure_zero_folders_are_returned()
            {
                Then(() =>
                    folders.Count().ShouldBe(0));
            }
        }

        [TestFixture]
        public class When_add_Folder_to_Project : ModelTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Project_is_created);
                And("It has a folder", () =>
                    project.Items.Add(new Folder() { Name = "Views" }));
            }

            [Test]
            public void assure_its_added_to_Items()
            {
                When("add new folder", () =>
                    project.NewFolder("Controllers"));

                Then(() =>
                {
                    project.Items.Count.ShouldBe(2);
                    project.Items.Last().Name.ShouldBe("Controllers");
                });
            }

            [Test]
            public void assure_Exception_is_thrown_if_Folder_already_exists()
            {
                When("add new folder and give it a name of an existing folder");

                Then(() =>
                    this.ShouldThrowException<ArgumentException>(() =>
                        project.NewFolder("Views"), ex =>
                            ex.Message.ShouldBe("Folder already exists")));
            }
        }

        [TestFixture]
        public class When_add_File_to_Project : ModelTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Project_is_created);
                And("it has a file", () =>
                    project.Items.Add(new File() { Name = "viewmodel.mvvm" }));
            }

            [Test]
            public void assure_its_added_to_Items()
            {
                When("add new file", () =>
                    project.NewFile("LoginViewModel.cs"));

                Then(() =>
                {
                    project.Items.Count.ShouldBe(2);
                    project.Items.Last().Name.ShouldBe("LoginViewModel.cs");
                });
            }

            [Test]
            public void assure_Exception_is_thrown_if_File_already_exists()
            {
                When("add new file and give it a name of an existing File");

                Then(() =>
                    this.ShouldThrowException<ArgumentException>(() =>
                        project.NewFile("viewmodel.mvvm"), ex =>
                            ex.Message.ShouldBe("File already exists")));
            }
        }

        public class ModelTestScenario : NUnitScenarioClass
        {
            protected static Project project;
            protected static IEnumerable<Folder> folders;

            protected Context Project_is_created = () =>
            {
                project = new Project();
            };

            protected When Project_is_spawned = () =>
            {
                project = new Project();
            };
        }
    }
}
