using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel;
using TinyMVVM.Tests.SemanticModel.TestContext;

namespace TinyMVVM.Tests.SemanticModel.ViewModelSpecs
{
    [TestFixture]
    public class When_spawning : ViewModelContext
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
                    new ViewModel(null)));
        }
    }
        
    [TestFixture]
    public class When_spawned : ViewModelContext
    {
        [SetUp]
        public void Setup()
        {
            When(ViewModel_is_spawned);
        }

        [Test]
        public void assure_it_has_a_Name()
        {
            Then(() =>
                 viewModel.Name.ShouldBe("LoginViewModel"));
        }   

        [Test]
        public void assure_it_Data()
        {
            Then(() =>
                 viewModel.Properties.ShouldNotBeNull());
        }

        [Test]
        public void assure_it_has_Commands()
        {
            Then(() =>
                 viewModel.Commands.ShouldNotBeNull());
        }
    }

    [TestFixture]
    public class When_adding_ViewModelData : ViewModelContext
    {
        [SetUp]
        public void Setup()
        {
            Given(ViewModel_is_created);
        }

        [Test]
        public void assure_Data_arg_is_validated()
        {
            When("adding ViewModelData");

            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    viewModel.AddViewModelData(null)));
        }

        [Test]
        public void assure_its_added()
        {
            var viewModelData = new ViewModelProperty("Username", typeof(string));

            When("adding ViewModelData", () =>
                viewModel.AddViewModelData(viewModelData));

            Then(() =>
                 viewModel.Properties.ShouldContain(viewModelData));
        }
    }

    [TestFixture]
    public class When_adding_a_ViewModelCommand : ViewModelContext
    {
        [SetUp]
        public void Setup()
        {
            Given(ViewModel_is_created);
        }

        [Test]
        public void assure_Command_arg_is_validated()
        {
            When("adding a ViewModelCommand");

            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    viewModel.AddViewModelCommand(null)));
        }

        [Test]
        public void assure_its_added()
        {
            var command = new ViewModelCommand("Login");

            When("adding a ViewModelCommand", () =>
                viewModel.AddViewModelCommand(command));

            Then(() =>
                 viewModel.Commands.ShouldContain(command));
        }
    }

}
