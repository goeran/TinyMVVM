using System;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model.VsSolution
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

    	[TestFixture]
    	public class When_Delete_file : FolderTestScenario
    	{
    		[SetUp]
    		public void Setup()
    		{
    			Given(Folder_is_created);
				And("It has a file", () =>
					folder.NewFile("View.xaml"));
    		}

    		[Test]
    		public void assure_File_is_deleted()
    		{
				When("Delete File", () =>
					folder.DeleteFile("View.xaml"));

    			Then(() => folder.HasFile("View.xaml").ShouldBeFalse());
    		}

    		[Test]
    		public void assure_Exception_is_thrown_if_file_does_not_exist()
    		{
    			When("Delete a file that does not exist");

				Then(() =>
					this.ShouldThrowException<ArgumentException>(() =>
						folder.DeleteFile("DoesNotExist.file"), ex =>
							ex.Message.ShouldBe("File doesn't exists")));
    		}
    	}

    	[TestFixture]
    	public class When_get_CurrentNamespace : FolderTestScenario
    	{
    		[SetUp]
    		public void Setup()
    		{
    			Given("VisualStudio Solution is created", () =>
    			{
    				solution = factory.NewSolution("Solution.sln");
    				project = solution.NewProject("Client");
    				project.RootNamespace = "Client";
    				folder = project.NewFolder("SubFolder").NewFolder("Controllers");
				});

    			When("get CurrentNamespace");
    		}

    		[Test]
    		public void assure_CurrentNamespace_for_project_is_the_same_as_RootNamespace()
    		{
    			Then(() =>
    			{
					project.CurrentNamespace.ShouldBe(project.RootNamespace);
    			});
    		}

    		[Test]
    		public void assure_CurrentNamespace_is_generated_based_on_Folder_structure_and_Projects_RootNamespace()
    		{
    			Then(() =>
    			{
    				folder.CurrentNamespace.ShouldBe("Client.SubFolder.Controllers");
    			});
    		}
    	}

        public class FolderTestScenario : NUnitScenarioClass
        {
            protected static ModelFactory factory = new ModelFactory();
            protected static Folder folder;
            protected static bool result;
            protected static Folder subFolder;
            protected static Folder subSubFolder;
            protected static Project project;
        	protected static string path;
        	protected static Solution solution;
        	protected static File file;

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
