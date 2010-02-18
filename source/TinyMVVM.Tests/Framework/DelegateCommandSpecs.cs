using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests.Framework.TestContext;
using TinyMVVM.Framework;

namespace TinyMVVM.Tests.Framework.DelegateCommandSpecs
{
    [TestFixture]
    public class When_spawning_with_Execute_arg : DelegateCommandContext
    {
        [SetUp]
        public void Setup()
        {
            When("spawning");
        }

        [Test]
        public void assure_Execute_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    new DelegateCommand(null)));
        }
    }

    [TestFixture]
    public class When_spawning_with_Execute_and_CanExecute_arg : DelegateCommandContext
    {
        [SetUp]
        public void Setup()
        {
            When("spawning");
        }

        [Test]
        public void assure_Execute_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    new DelegateCommand(null, () => true)));

        }

        [Test]
        public void assure_CanExecute_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    new DelegateCommand(() => { }, null)));
        }
    }

    [TestFixture]
    public class When_spawned : DelegateCommandContext
    {
        [SetUp]
        public void Setup()
        {
            When(DelegateCommand_is_spawned);
        }

        [Test]
        public void assure_its_a_ICommand()
        {
            Then(() =>
                 (delegateCommand is ICommand).ShouldBeTrue());
        }
    }

    [TestFixture]
    public class When_Execute : DelegateCommandContext
    {
        [Test]
        public void assure_Execute_delegate_is_called()
        {
            Given(DelegateCommand_is_created);

            When("Execute", () =>
                delegateCommand.Execute(null));

            Then(() => executeDelegateIsCalled.ShouldBeTrue());
        }

        [Test]
        public void assure_CanExecute_delegate_is_called()
        {
            Given(DelegateCommand_is_created);

            When("Execute", () =>
                delegateCommand.Execute(null));

            Then(() => canExecuteDelegateIsCalled.ShouldBeTrue());
        }

        [Test]
        public void assure_Execute_delegate_is_not_called()
        {
            Given("DelegateCommand is created").And("CanExecute delegate returns false", () =>
            {
                executeDelegateIsCalled = false;
                delegateCommand = new DelegateCommand(() =>
                {
                    executeDelegateIsCalled = true;
                }, () => { return false; });
            });

            When("Execute", () => delegateCommand.Execute(null));

            Then(() => executeDelegateIsCalled.ShouldBeFalse());
        }
    }

}
