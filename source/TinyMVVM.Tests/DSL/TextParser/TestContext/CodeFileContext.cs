using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DSL.TextParser;

namespace TinyMVVM.Tests.DSL.TextParser.TestContext
{
    public class CodeFileContext : NUnitScenarioClass
    {
        protected static string filePathToCodeFile = Path.Combine(
            Environment.CurrentDirectory, "CodeFile.txt");

        protected static CodeFile codeFile;
        protected static string code;

        protected Context CodeFile_instance_is_created = () =>
        {
            code = null;
            codeFile = new CodeFile(filePathToCodeFile);
        };

        protected Context CodeFile_exists = () =>
        {
            File.Create(filePathToCodeFile).Close();
        };
    }
}
