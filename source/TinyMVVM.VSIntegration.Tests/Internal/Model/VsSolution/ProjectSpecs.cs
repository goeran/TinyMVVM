﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model.VsSolution
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
                Then(() => project.Name.ShouldNotBeNull());
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

            [Test]
            public void assure_Project_is_set()
            {
                Then(() => project.Project.ShouldBe(project));
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
                    project.NewFolder("Views"));
            }

            [Test]
            public void assure_its_added_to_Items()
            {
                When(add_new_folder("Controllers"));

                Then(() =>
                {
                    project.Items.Count.ShouldBe(2);
                    project.Items.Last().Name.ShouldBe("Controllers");
                });
            }

            [Test]
            public void assure_Parent_is_set()
            {
                When(add_new_folder("Controllers"));

                Then(() =>
                {
                    newFolder.Parent.ShouldBe(project);
                });
            }

            [Test]
            public void assure_Project_is_set()
            {
                When(add_new_folder("Controllers"));

                Then(() =>
                     newFolder.Project.ShouldBe(project));
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
                    project.NewFile("viewmodel.mvvm"));
            }

            [Test]
            public void assure_its_added_to_Items()
            {
                When(add_new_file);

                Then(() =>
                {
                    project.Items.Count.ShouldBe(2);
                    project.Items.Last().Name.ShouldBe("LoginViewModel.cs");
                });
            }

            [Test]
            public void assure_Parent_is_set()
            {
                When(add_new_file);

                Then(() =>
                    newFile.Parent.ShouldBe(project));
            }

            [Test]
            public void assure_Project_is_set()
            {
                When(add_new_file);

                Then(() =>
                     newFile.Project.ShouldBe(project));
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
            private static ModelFactory factory = new ModelFactory();
            protected static Project project;
            protected static IEnumerable<Folder> folders;
            protected static Folder newFolder;
            protected static File newFile;

            protected Context Project_is_created = () =>
            {
                NewProject();
            };

            private static void NewProject()
            {
				var solution = factory.NewSolution(System.IO.Path.Combine(Environment.CurrentDirectory, "Test.sln"));
            	solution.Path = Environment.CurrentDirectory;
                project = solution.NewProject("RichRememberTheMilk.proj");
                project.DirectoryPath = Environment.CurrentDirectory;
            }

            protected When Project_is_spawned = () =>
            {
                NewProject();
            };

            protected When add_new_file = () =>
            {
                newFile = project.NewFile("LoginViewModel.cs");
            };

            protected WhenSemantics add_new_folder(string name)
            {
                return When("add new folder", () =>
                    newFolder = project.NewFolder("Controllers"));
            }
        }
    }
}
