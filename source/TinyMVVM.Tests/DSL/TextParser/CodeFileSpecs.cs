using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DSL.Framework;
using TinyMVVM.Tests.DSL.TextParser.TestContext;
using TinyMVVM.DSL.TextParser;
using System.IO;

namespace TinyMVVM.Tests.DSL.TextParser.CodeFileSpecs
{
    [TestFixture]
    public class When_spawning : CodeFileContext
    {
        [SetUp]
        public void Setup()
        {
            When("spawning");
        }

        [Test]
        public void assure_FilePath_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    new CodeFile(null)));
        }

        [Test]
        public void assure_FilePath_is_checked_on_FileSystem()
        {
            Then(() =>
                this.ShouldThrowException<FileNotFoundException>(() =>
                    new CodeFile("t:\thisfiledoesntexist.txt")));
        }
    }

    [TestFixture]
    public class When_loading_code : CodeFileContext
    {
        [SetUp]
        public void Setup()
        {
            Given(CodeFile_exists);
            And(CodeFile_instance_is_created);

            When("loading code", () =>
                code = codeFile.Load());
        }

        [Test]
        public void assure_code_is_loaded()
        {
            Then(() =>
                 code.ShouldNotBeNull());
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(filePathToCodeFile);
        }
    }

}
