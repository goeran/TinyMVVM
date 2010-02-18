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
    public class When_Parse_before_code_is_loaded : ParserContext
    {
        [SetUp]
        public void Setup()
        {
            Given(Parser_is_created);

            When("parse");
        }
    }

    [TestFixture]
    public class When_Parse : ParserContext
    {
        private string code;

        [SetUp]
        public void Setup()
        {
            Given("a simple vm is described with the MVVM dsl", () =>
            {
                code = "vm LoginViewModel:\n" +
                       "\tdata Username as string\n\r" +
                       "\tdata Password as string\n" +
                       "\t\tcommand Login\n" +
                       "\tcommand Cancel\n" +
                       "" +
                       "vm Search:\n" +
                       "\tcommand Search" +
                       "\tdata Query as string\n";
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
                semanticModel.ViewModels.ShouldHave(2);
            });
        }

        [Test]
        public void assure_ViewModel_Name_is_parsed()
        {
            Then(() =>
            {
                semanticModel.ViewModels[0].Name.ShouldBe("LoginViewModel");
                semanticModel.ViewModels[1].Name.ShouldBe("Search");
            });
        }

        [Test]
        public void assure_ViewModelData_is_parsed()
        {
            Then(() =>
            {
                var vm = semanticModel.ViewModels.First();
                vm.Data[0].Name.ShouldBe("Username");
                vm.Data[0].Type.ShouldBe(typeof(string));
                vm.Data[1].Name.ShouldBe("Password");
                vm.Data[1].Type.ShouldBe(typeof(string));

                var vmSearch = semanticModel.ViewModels[1];
                vmSearch.Data[0].Name.ShouldBe("Query");
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

                var vmSearch = semanticModel.ViewModels[1];
                vmSearch.Commands[0].Name.ShouldBe("Search");
            });
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
