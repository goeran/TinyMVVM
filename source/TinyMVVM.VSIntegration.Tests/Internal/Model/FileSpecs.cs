using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model
{
    public class FileSpecs
    {
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
    }

    public class FileSpecsTestScenario : ScenarioClass<FileSpecsTestScenario>
    {
        protected File file;

        public void File_is_created()
        {
            file = new File();
        }
    }
}
