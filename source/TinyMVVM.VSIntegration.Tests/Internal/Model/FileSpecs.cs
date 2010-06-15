using System;
using System.IO;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using File = TinyMVVM.TinyMVVM_VSIntegration.Internal.Model.File;

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
                Given.File_on_disk_exists();

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
        public class When_create_new_CodeBehind_File : FileSpecsTestScenario
        {
            [SetUp]
            public void Setup()
            {
                Given.File_is_created();
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
                Given.File_on_disk_exists();
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
        protected File file;
        private string path;

        public void File_on_disk_exists()
        {
            path = Path.Combine(Environment.CurrentDirectory, "test.txt");
            using (var f = System.IO.File.Create(path));
        }

        public void File_is_created()
        {
            File_is_spawned();
        }

        public void File_is_spawned()
        {
            file = new File();
            file.Path = path;
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
