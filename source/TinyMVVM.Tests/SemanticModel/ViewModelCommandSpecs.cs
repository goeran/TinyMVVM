using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.Tests.SemanticModel.TestContext;

namespace TinyMVVM.Tests.SemanticModel.ViewModelCommandSpecs
{
    [TestFixture]
    public class When_spawning : ViewModelCommandContext
    {
        [SetUp]
        public void Setup()
        {
            When("spawning");
        }

        [Test]
        public void assure_Name_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    new ViewModelCommand(null)));
        }
    }

    [TestFixture]
    public class When_spawned : ViewModelCommandContext
    {
        [SetUp]
        public void Setup()
        {
            When("ViewModelCommand is spawned", () =>
                viewModelCommand = new ViewModelCommand("Login"));
        }

        [Test]
        public void assure_it_has_a_Name()
        {
            Then(() =>
                 viewModelCommand.Name.ShouldBe("Login"));
        }
    }
}
