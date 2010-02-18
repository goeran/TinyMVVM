using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DSL.TextParser;
using TinyMVVM.SemanticModel;

namespace TinyMVVM.Tests.DSL.TextParser.TestContext
{
    public class ParserContext : NUnitScenarioClass
    {
        public static Parser parser;
        protected static ModelSpecification semanticModel;

        protected Context Parser_is_created = () =>
        {
            parser = new Parser();
        };
    }
}
