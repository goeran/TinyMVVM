using System;
using System.Linq;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using System.IO;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using File = TinyMVVM.VSIntegration.Internal.Model.VsSolution.File;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model.VsSolution
{
    public class FileSpecs
    {
        [TestFixture]
        public class When_spawned : FileSpecsTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given.Folder_is_created();
                And.File_on_disk_exists();

                When.File_is_spawned();
            }

            [Test]
            public void Then_assure_it_has_Content()
            {
                file.Content.ShouldNotBeNull();
            }

            [Test]
            public void Then_assure_it_has_a_Stream()
            {
                var stream = file.NewFileStream();
                stream.ShouldNotBeNull();
                stream.Close();
                stream.Dispose();
            }
        }

        [TestFixture]
        public class When_File_on_disk_does_not_exists : FileSpecsTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given.Folder_is_created();
                And.File_is_created();
                And.File_on_disk_does_not_exists();

                When.Write_to_file_stream("hello world");
            }

            [Test]
            public void Then_assure_file_is_created()
            {
                TestExtensions.ShouldBeTrue(System.IO.File.Exists(file.Path));
            }
        }

        [TestFixture]
        public class When_create_new_CodeBehind_File : FileSpecsTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given.Folder_is_created();
                And.File_is_created();
            }

            [Test]
            public void Then_assure_fileName_arg_is_validated()
            {
                this.ShouldThrowException<ArgumentNullException>(() =>
                    file.NewCodeBehindFile(null));   
            }
        }

    	[TestFixture]
    	public class When_Delete_CodeBehind_File : FileSpecsTestScenario
    	{
    		[SetUp]
    		public void Setup()
    		{
    			Given.Folder_is_created();
				And.File_is_created();
				And.CodeBehindFile_is_added(filename + ".cs");

				When.Delete_CodeBehind_File(filename + ".cs");
    		}

    		[Test]
    		public void Then_assure_CodeBehind_File_is_deleted()
    		{
    			file.CodeBehindFiles.Count().ShouldBe(0);
    		}
    	}

    	[TestFixture]
    	public class When_Delete_a_CodeBehind_File_that_doesnt_exist : FileSpecsTestScenario
    	{
    		[SetUp]
    		public void Setup()
    		{
    			Given.Folder_is_created();
				And.File_is_created();
    		}

    		[Test]
    		public void Then_assure_Exception_is_thrown()
    		{
    			this.ShouldThrowException<ArgumentException>(() =>
					When.Delete_CodeBehind_File("FileDoesntExist.txt"), ex =>
						ex.Message.ShouldBe("Code Behind File doesn't exist"));
    		}
    	}

        [TestFixture]
        public class When_write_to_file_Stream : FileSpecsTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given.Folder_is_created();
                And.File_is_created();

                When.Write_to_file_stream("hello world");
            }

            [Test]
            public void Then_assure_file_Content_is_updated()
            {
                file.Content.ShouldBe("hello world");
            }
        }
    }

    public class FileSpecsTestScenario : ScenarioClass<FileSpecsTestScenario>
    {
        private ModelFactory factory = new ModelFactory();
        protected Folder folder;
        protected File file;
    	protected const string filename = "test.txt";

        public void Folder_is_created()
        {
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "ViewModel"));

        	var solution = new ModelFactory().NewSolution(System.IO.Path.Combine(Environment.CurrentDirectory, "Test.sln"));
        	solution.Path = Environment.CurrentDirectory;
            var proj = solution.NewProject("Project");
            proj.DirectoryPath = Environment.CurrentDirectory;
            folder = proj.NewFolder("ViewModel");
        }

        public void File_on_disk_does_not_exists()
        {
            if (System.IO.File.Exists(file.Path))
                System.IO.File.Delete(file.Path);
        }

        public void File_on_disk_exists()
        {
            var path = Path.Combine(folder.Path, "test.txt");
            using (var f = System.IO.File.Create(path));
        }

        public void File_is_created()
        {
            File_is_spawned();
        }

        public void File_is_spawned()
        {
            file = folder.NewFile(filename);
        }

        public void Write_to_file_stream(string content)
        {
            using (var sw = file.NewFileStream())
            {
                sw.Write(content);
            }
        }

    	public void Delete_CodeBehind_File(string name)
    	{
    		file.DeleteCodeBehindFile(name);
    	}

		public void CodeBehindFile_is_added(string name)
		{
			file.NewCodeBehindFile(name);
		}
    }
}
