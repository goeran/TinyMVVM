using System;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Factories;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;
using IO = System.IO;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model
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
                IO.File.Exists(file.Path).ShouldBeTrue();
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

        public void Folder_is_created()
        {
            IO.Directory.CreateDirectory(IO.Path.Combine(Environment.CurrentDirectory, "ViewModel"));

            var proj = factory.NewProject();
            proj.DirectoryPath = Environment.CurrentDirectory;
            folder = proj.NewFolder("ViewModel");
        }

        public void File_on_disk_does_not_exists()
        {
            if (IO.File.Exists(file.Path))
                IO.File.Delete(file.Path);
        }

        public void File_on_disk_exists()
        {
            var path = IO.Path.Combine(folder.Path, "test.txt");
            using (var f = System.IO.File.Create(path));
        }

        public void File_is_created()
        {
            File_is_spawned();
        }

        public void File_is_spawned()
        {
            file = folder.NewFile("test.txt");
        }

        public void Write_to_file_stream(string content)
        {
            using (var sw = file.NewFileStream())
            {
                sw.Write(content);
            }
        }
    }
}
