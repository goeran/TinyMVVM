using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DSL.TextParser;
using TinyMVVM.Tests.DSL.TextParser.TestContext;
using Moq;
using System.IO;

namespace TinyMVVM.Tests.DSL.TextParser.ParserSpecs
{
    [TestFixture]
    public class When_spawning_with_specific_LexicalAnalyzer : ParserContext
    {
        [SetUp]
        public void Setup()
        {
            When("spawning with specific LexicalAnalyzer");
        }

        [Test]
        public void assure_scanner_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    new Parser(null)));
        }
    }

    [TestFixture]
    public class When_Parse : ParserContext
    {
        [SetUp]
        public void Setup()
        {
            Given("a simple viewmodel is described with the MVVM dsl", () =>
            {
                code = "viewmodel LoginViewModel:\n" +
                       "\t[Required]\n" +
                       "\t[MaxLength(10)]\n" +
                       "\tproperty Username as string\n\r" +
                       "\tproperty Password as string\n" +
                       "\t\tcommand Login\n" +
                       "\tcommand Cancel\n" +
                       "" +
                       "viewmodel SearchQuery:\n" +
                       "\tproperty Query as string\n" +
                       "\tproperty Length as int\n" +
                       "" +
                       "viewmodel Search:\n" +
                       "\tcommand Search" +
                       "\toproperty Query as SearchQuery\n";
            });
            And(Parser_is_created);

            When("parse", () =>
                semanticModel = parser.Parse(new InlineCode(code)));
        }

        [Test]
        public void assure_ViewModels_are_parsed()
        {
            Then(() =>
            {   
                semanticModel.ViewModels.ShouldHave(3);
            });
        }

        [Test]
        public void assure_ViewModel_Name_is_parsed()
        {
            Then(() =>
            {
                semanticModel.ViewModels[0].Name.ShouldBe("LoginViewModel");
                semanticModel.ViewModels[1].Name.ShouldBe("SearchQuery");
                semanticModel.ViewModels[2].Name.ShouldBe("Search");
            });
        }

        [Test]
        public void assure_ViewModel_Properties_are_parsed()
        {
            Then(() =>
            {
                var vm = semanticModel.ViewModels.First();
                vm.Properties[0].Name.ShouldBe("Username");
                vm.Properties[0].Type.ShouldBe("string");
                vm.Properties[0].IsObservable.ShouldBeFalse();
                vm.Properties[1].Name.ShouldBe("Password");
                vm.Properties[1].Type.ShouldBe("string");
                vm.Properties[1].IsObservable.ShouldBeFalse();

                var vmSearch = semanticModel.ViewModels[2];
                vmSearch.Properties[0].Name.ShouldBe("Query");
                vmSearch.Properties[0].IsObservable.ShouldBeTrue();
            });
        }

        [Test]
        public void assure_Attributes_on_ViewModel_Properties_are_parsed()
        {
            Then(() =>
            {
                var vm = semanticModel.ViewModels.First();
                var usernameProperty = vm.Properties[0];

                usernameProperty.Attributes.ShouldHave(2);
                usernameProperty.Attributes.Where(a => a == "[Required]").Count().ShouldBe(1);
                usernameProperty.Attributes.Where(a => a == "[MaxLength(10)]").Count().ShouldBe(1);
            });
        }


        [Test]
        public void assure_ViewModelCommand_is_parsed()
        {
            Then(() =>
            {
                var vm = semanticModel.ViewModels.First();
                vm.Commands[0].Name.ShouldBe("Login");
                vm.Commands[1].Name.ShouldBe("Cancel");

                var vmSearch = semanticModel.ViewModels[2];
                vmSearch.Commands[0].Name.ShouldBe("Search");
            });
        }
    }

    [TestFixture]
    public class When_Parse_and_checking_grammar : ParserContext
    {
        [SetUp]
        public void Setup()
        {
            Given(Parser_is_created);
        }

        [Test]
        public void assure_it_checks_if_Name_is_specified_for_ViewModel()
        {
            And(dsl_code_is_described(
                "viewmodel:\n" +
                "\tproperty as string"));

            When("parse");

            Then(() =>
                this.ShouldThrowException<InvalidSyntaxException>(() =>
                    parser.Parse(new InlineCode(code)), ex =>
                        ex.Message.ShouldBe("Name must be specified when using the 'viewmodel' keyword")));
        }

        [Test]
        public void assure_it_checks_if_Name_is_specified_for_property()
        {
            And(dsl_code_is_described(
                "viewmodel Login:\n" +
                "\tproperty"));

            When("parse");

            Then(() =>
                this.ShouldThrowException<InvalidSyntaxException>(() =>
                    parser.Parse(new InlineCode(code)), ex =>
                        ex.Message.ShouldBe("Name must be specified when using the 'property' keyword")));
        }

        [Test]
        public void assure_it_checks_if_Type_is_specified_for_property()
        {
            And(dsl_code_is_described(
                "viewmodel Login:\n" +
                "\tproperty Username"));

            When("parse");

            Then(() =>
                this.ShouldThrowException<InvalidSyntaxException>(() =>
                    parser.Parse(new InlineCode(code)), ex =>
                        ex.Message.ShouldBe("Type must be specified when using the 'property' keyword")));
        }

        [Test]
        public void assure_it_checks_if_Name_is_specified_for_command()
        {
            And(dsl_code_is_described(
                "viewmodel Login:\n" +
                "\tcommand"));

            When("parse");

            Then(() =>
                this.ShouldThrowException<InvalidSyntaxException>(() =>
                    parser.Parse(new InlineCode(code)), ex =>
                        ex.Message.ShouldBe("Name must be specified when using the 'command' keyword")));
        }
    }

    [TestFixture]
    public class When_loading_Code : ParserContext
    {
        [SetUp]
        public void Setup()
        {
            Given(Parser_is_created);
        }

        [Test]
        public void assure_LoadingStrategy_arg_is_validated()
        {
            When("loading Code");

            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    parser.Parse(null)));
        }

    }
}
