﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model
{
    public class FolderSpecs
    {
        [TestFixture]
        public class When_DirectoryPath_is_not_set : FolderTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Folder_is_created);
                And("Path is not set", () =>
                    folder.DirectoryPath = null);
            }

            [Test]
            public void assure_Exception_is_thrown_when_create_SubFolder()
            {
                When("create new SubFolder");

                Then(() =>
                    this.ShouldThrowException<InvalidOperationException>(() =>
                        folder.NewFolder("hello")));
            }

            [Test]
            public void assure_Exception_is_thrown_when_create_File()
            {
                When("create new File");

                Then(() =>
                    this.ShouldThrowException<InvalidOperationException>(() =>
                        folder.NewFile("test.txt")));
            }
        }

        [TestFixture]
        public class When_check_if_SubFolder_exists : FolderTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Folder_is_created);
                And("it has a sub folder", () =>
                    folder.NewFolder("Views"));

                When("check if same sub Folder exists", () =>
                    result = folder.HasFolder("Views"));
            }

            [Test]
            public void assure_result_is_true()
            {
                Then(() => result.ShouldBeTrue());
            }
        }

        [TestFixture]
        public class When_get_sub_Folder_by_name : FolderTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Folder_is_created);
                And("it has a sub folder", () =>
                    folder.NewFolder("Views"));
            }

            [Test]
            public void assure_sub_Folder_is_returned()
            {
                When("get sub folder by name", () =>
                    subFolder = folder.GetSubFolder("Views"));

                Then(() => subFolder.ShouldNotBeNull());
            }

        	[Test]
        	public void assure_DirectoryPath_and_Path_is_set()
        	{
				When("get sub folder by name", () =>
					subFolder = folder.GetSubFolder("Views"));

        		Then(() =>
        		{
					subFolder.DirectoryPath.ShouldBe(System.IO.Path.Combine(folder.DirectoryPath, "Views"));
					subFolder.Path.ShouldBe(subFolder.DirectoryPath);
        		});
        	}

            [Test]
            public void assure_Exception_is_thrown_if_sub_folder_doesnt_exist()
            {
                When("get sub folder by name");

                Then(() =>
                    this.ShouldThrowException<ArgumentException>(() =>
                        folder.GetSubFolder("foobar"), ex =>
                            ex.Message.ShouldBe("Folder doesn't exists")));
            }
        }

        [TestFixture]
        public class When_find_Project_from_a_SubFolder : FolderTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given(Project_is_created);
                And("it has a sub folder", () =>
                    subSubFolder = project.NewFolder("Views").NewFolder("Login"));

                When("find Project from a SubFolder");
            }

            [Test]
            public void assure_Project_is_found()
            {
                Then(() => subSubFolder.Project.ShouldBe(project));
            }
        }

    	[TestFixture]
    	public class When_get_Path_for_folder : FolderTestScenario
    	{
    		[SetUp]
    		public void Setup()
    		{
    			Given(Project_is_created);
    			And(Folder_is_created);

				When("Get Path", () =>
					path = folder.Path);
    		}

    		[Test]
    		public void assure_Path_is_the_same_as_DirectoryPath()
    		{
    			Then(() =>
    			{
    				path.ShouldBe(folder.DirectoryPath);
    			});
    		}
    	}

        public class FolderTestScenario : NUnitScenarioClass
        {
            private static ModelFactory factory = new ModelFactory();
            protected static Folder folder;
            protected static bool result;
            protected static Folder subFolder;
            protected static Folder subSubFolder;
            protected static Project project;
        	protected static string path;

            protected Context Project_is_created = () =>
            {
                NewProject();
            };

            private static void NewProject()
            {
            	var solution = factory.NewSolution(System.IO.Path.Combine(Environment.CurrentDirectory, "Test.sln"));
            	solution.Path = Environment.CurrentDirectory;
                project = solution.NewProject("RichRememberTheMilk");
            	project.DirectoryPath = System.IO.Path.Combine(Environment.CurrentDirectory, "RichRememberTheMilk");
            }

            protected Context Folder_is_created = () =>
            {
                NewProject();

                folder = project.NewFolder("ViewModel");
            };
        }
    }
}
