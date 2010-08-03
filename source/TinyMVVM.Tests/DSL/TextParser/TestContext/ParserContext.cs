using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DSL.Framework;
using TinyMVVM.DSL.TextParser;
using TinyMVVM.SemanticModel.MVVM;

namespace TinyMVVM.Tests.DSL.TextParser.TestContext
{
    public class ParserContext : NUnitScenarioClass
    {
        public static Parser parser;
        protected static ModelSpecification semanticModel;
        protected static string code;

        protected Context Parser_is_created = () =>
        {
            parser = new Parser();
        };

        protected AndSemantics dsl_code_is_described(string dslCode)
        {
            return And("dsl code is described", () =>
                code = dslCode);
        }

        protected When parse = () =>
        {
            semanticModel = parser.Parse(new InlineCode(code));
        };
    }
}
