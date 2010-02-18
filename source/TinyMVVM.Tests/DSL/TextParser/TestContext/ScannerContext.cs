using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DSL.TextParser;

namespace TinyMVVM.Tests.DSL.TextParser.TestContext
{
    public class ScannerContext : NUnitScenarioClass
    {
        protected static Scanner scanner;
        protected static string code;
        protected static IEnumerable<Token> tokens;
        
        protected Context Scanner_is_created = () =>
        {
            scanner = new Scanner();
        };
    }
}
