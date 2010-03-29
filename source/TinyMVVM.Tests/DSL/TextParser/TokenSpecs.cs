using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DSL.TextParser;
using TinyMVVM.Tests.DSL.TextParser.TestContext;

namespace TinyMVVM.Tests.DSL.TextParser.TokenSpecs
{
    [TestFixture]
    public class When_spawning_Token : TokenContext
    {
        [SetUp]
        public void Setup()
        {
            When("spawning Token");
        }

        [Test]
        public void assure_Name_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    Token.Name(null)));
        }
    }

    [TestFixture]
    public class When_Name_Token_is_spawned : TokenContext
    {
        [SetUp]
        public void Setup()
        {
            When("spawned", () =>
                token = Token.Name("LoginViewModel"));
        }

        [Test]
        public void assure_Name_is_set()
        {
            Then(() =>
                token.Value.ShouldBe("LoginViewModel"));
        }

        [Test]
        public void assure_Kind_is_Name()
        {
            Then(() =>
                 token.Kind.ShouldBe(Kind.Name));
        }
    }

    [TestFixture]
    public class When_Keyword_Token_is_spawned : TokenContext
    {
        [SetUp]
        public void Setup()
        {
            When("viewmodel Token is spawned", () =>
                token = Token.Keyword(Kind.viewmodel));
        }

        [Test]
        public void assure_Kind_is_set_to_ViewModel()
        {
            Then(() =>
                 token.Kind.ShouldBe(Kind.viewmodel));
        }
    }

    [TestFixture]
    public class When_compare_Name_Tokens : TokenContext
    {
        [Test]
        public void compare_equals_tokens()
        {
            var tokenA = Token.Name("LoginViewModel");
            var tokenB = Token.Name("LoginViewModel");

            tokenA.Equals(tokenB).ShouldBeTrue();
            (tokenA == tokenB).ShouldBeTrue();
            (tokenA != tokenB).ShouldBeFalse();
        }

        [Test]
        public void compare_equals_Keyword_tokens()
        {
            var tokenA = Token.Keyword(Kind.viewmodel);
            var tokenB = Token.Keyword(Kind.viewmodel);

            tokenA.Equals(tokenB).ShouldBeTrue();
            (tokenA == tokenB).ShouldBeTrue();
            (tokenA != tokenB).ShouldBeFalse();
        }


        [Test]
        public void compare_non_equals_Name_tokens()
        {
            var tokenA = Token.Name("LoginViewModel");
            var tokenB = Token.Name("SearchViewModel");

            tokenA.Equals(tokenB).ShouldBeFalse();
            (tokenA == tokenB).ShouldBeFalse();
            (tokenA != tokenB).ShouldBeTrue();
        }

        [Test]
        public void compare_non_equals_Keyword_tokens()
        {
            var tokenA = Token.Keyword(Kind.viewmodel);
            var tokenB = Token.Keyword(Kind.EOF);

            tokenA.Equals(tokenB).ShouldBeFalse();
            (tokenA == tokenB).ShouldBeFalse();
            (tokenA != tokenB).ShouldBeTrue();

        }

        [Test]
        public void when_compare_token_with_another_object()
        {
            var tokenA = Token.Name("LoginViewModel");
            var tokenB = "LoginViewModel";

            tokenA.Equals(tokenB).ShouldBeFalse();
        }

    }

    [TestFixture]
    public class When_spawning_a_predefined_Token : TokenContext
    {
        [Test]
        public void assure_its_a_ViewModel_token()
        {
            When("spawning viewmodel token", () =>
                token = Token.ViewModel);

            Then(() =>
                token.ShouldBe(Token.Keyword(Kind.viewmodel)));
        }

        [Test]
        public void assure_its_a_property_token()
        {
            When("spawning a property token", () =>
                token = Token.Property);

            Then(() =>
                 token.ShouldBe(Token.Keyword(Kind.property)));
        }

        [Test]
        public void assure_its_a_oproperty_token()
        {
            When("spawning a property token", () =>
                token = Token.OProperty);

            Then(() =>
                token.ShouldBe(Token.Keyword(Kind.oproperty)));
        }

        [Test]
        public void assure_its_a_Command_token()
        {
            When("spawning a command token", () =>
                token = Token.Command);

            Then(() =>
                 token.ShouldBe(Token.Keyword(Kind.command)));
        }

        [Test]
        public void assure_its_a_EOF_token()
        {
            When("spawning a EOF token", () =>
                token = Token.EOF);

            Then(() =>
                 token.ShouldBe(Token.Keyword(Kind.EOF)));
        }

        [Test]
        public void assure_its_a_Attribute_token()
        {
            When("spawning a Attribute token", () =>
                token = Token.Attribute("Required"));

            Then(() =>
                token.ShouldBe(Token.Attribute("Required")));
        }
   
    }
}
