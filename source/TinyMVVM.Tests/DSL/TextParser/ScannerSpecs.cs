using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DSL.TextParser;
using TinyMVVM.Tests.DSL.TextParser.TestContext;

namespace TinyMVVM.Tests.DSL.TextParser.ScannerSpecs
{
    [TestFixture]
    public class When_Scan : ScannerContext
    {
        [SetUp]
        public void Setup()
        {
            Given(Scanner_is_created);
            And("simple vm is described with the MVVM DSL", () =>
                code = "vm LoginViewModel:\n\tdata Username\n\r\t\t\r\ncommand Login");

            When("scan", () =>
                tokens = scanner.Scan(code));
        }

        [Test]
        public void assure_Scanner_is_Mutable()
        {
            Then(() =>
            {
                var newTokens = scanner.Scan(code);

                (tokens == newTokens).ShouldBeFalse();
                newTokens.Count().ShouldBe(tokens.Count());
            });
        }

        [Test]
        public void assure_ViewModel_Token_is_found()
        {
            Then(() =>
                tokens.First().ShouldBe(Token.ViewModel));
        }

        [Test]
        public void assure_ViewModel_Name_Token_is_found()
        {
            Then(() =>
                 tokens.Where(t => t == Token.Name("LoginViewModel")).Count().ShouldBe(1));
        }

        [Test]
        public void assure_EOF_Token_is_found()
        {   
            Then(() =>
                 tokens.Last().ShouldBe(Token.EOF));
        }

        [Test]
        public void assure_Data_Token_is_found()
        {
            Then(() =>
                 tokens.Where(t => t == Token.Data).Count().ShouldBe(1));
        }

        [Test]
        public void assure_Data_Name_Token_is_found()
        {
            Then(() =>
                 tokens.Where(t => t == Token.Name("Username")).Count().ShouldBe(1));
        }

        [Test]
        public void assure_Command_Token_is_found()
        {
            Then(() =>
                 tokens.Where(t => t == Token.Command).Count().ShouldBe(1));
        }

        [Test]
        public void assure_Command_Name_Token_is_found()
        {
            Then(() =>
                 tokens.Where(t => t == Token.Name("Login")).Count().ShouldBe(1));
        }
    }

}
