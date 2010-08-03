using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DSL.Framework;
using TinyMVVM.DSL.TextParser;
using Parser = TinyMVVM.DSL.CodeGeneratorConfig.Parser;

namespace TinyMVVM.Tests.DSL.CodeGeneratorConfig
{
    class ParserSpecs
    {
        [TestFixture]
        public class When_Parse_without_valid_args : Shared
        {
            [SetUp]
            public void Setup()
            {
                Given(Parser_is_created);

                When("parse");
            }

            [Test]
            public void assure_LoadingStrategy_arg_is_valided()
            {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        parser.Parse(null)));
            }
        }

        [TestFixture]
        public class When_Parse_valid_code : Shared
        {
            [SetUp]
            public void Setup()
            {
                Given(Parser_is_created);
                And("Code is valid", () =>
                    code = "configure do\n" +
                           "\t generate \"views\" \n" +
                           "\t generate \"controllers\" \n" +
                           "end");

                When(Parse);
            }

            [Test]
            public void assure_SemanticModel_is_returned()
            {
                Then(() =>
                     semanticModel.ShouldNotBeNull());
            }

            [Test]
            public void assure_configuration_key_is_parsed()
            {
                Then(() =>
                    semanticModel.Contains("views").ShouldBeTrue());
            }

            [Test]
            public void assure_several_configuration_keys_can_be_parsed()
            {
                Then(() =>
                {
                    semanticModel.Contains("controllers").ShouldBeTrue();
                    semanticModel.Contains("views").ShouldBeTrue();
                });
            }
        }

        [TestFixture]
        public class When_Parse_invalid_code : Shared
        {
            [SetUp]
            public void Setup()
            {
                Given(Parser_is_created);
            }

            [Test]
            public void assure_InvalidSyntaxException_is_thrown()
            {
                And("Code is invalid", () =>
                    code = "configurea do\n" +
                           "end\n");

                When("Parse");

                Then(() =>
                    this.ShouldThrowException<InvalidSyntaxException>(() =>
                        Parse(), ex =>
                            Console.WriteLine(ex.Message)));
            }

            [Test]
            public void assure_InvalidSyntaxException_is_thrown_when_syntax_is_wrong()
            {
                And("code contians invalid syntax", () =>
                    code = "configure '..ASDa%€€€€££\n" +
                           "end\n");

                When("Parse");

                Then(() =>
                    this.ShouldThrowException<InvalidSyntaxException>(() =>
                        Parse(), ex =>
                            Console.WriteLine(ex)));
            }
        }

        //TODO: figure out what do to when allowing crazy stuff like this
        [TestFixture]
        public class When_Parse_code_that_opens_a_file : Shared
        {
            private string filePath = Path.Combine(Environment.CurrentDirectory, "temp.txt");

            [SetUp]
            public void Setup()
            {
                Given(Parser_is_created);
                And("Code is valid", () =>
                    code = string.Format("File.open '{0}'\n", filePath));
                And("File exist", () =>
                {
                    using (var fs = File.Create(filePath));
                });

                When(Parse);
            }

            [Test]
            public void assure_nothing_happens()
            {
                Then(() => { });
            }
        }

        public class Shared : NUnitScenarioClass
        {
            protected static Parser parser;
            protected static string code;
            protected static HashSet<string> semanticModel;

            protected Context Parser_is_created = () =>
            {
                parser = new Parser();
            };

            protected When Parse = () =>
            {
                semanticModel = parser.Parse(new InlineCode(code));
            };
        }
    }
}
