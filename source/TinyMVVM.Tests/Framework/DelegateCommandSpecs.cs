using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests.Framework.TestContext;
using TinyMVVM.Framework;

namespace TinyMVVM.Tests.Framework
{
    public class DelegateCommandSpecs
    {
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

            [Test]
            public void assure_it_has_Text()
            {
                Then(() =>
                    delegateCommand.Text.ShouldBeNull());
            }

            [Test]
            public void assure_it_has_Description()
            {
                Then(() =>
                    delegateCommand.Description.ShouldBeNull());
            }
        }

        [TestFixture]
        public class When_Text_is_changed : DelegateCommandContext
        {
            [SetUp]
            public void Setup()
            {
                Given(DelegateCommand_is_created);

                When("Text is changed", () =>
                    delegateCommand.Text = "Login");
            }

            [Test]
            public void assure_Observers_are_notified_about_state_change()
            {
                Then(() =>
                {
                    changeRecorder.Data.Where(r => r.PropertyName == "Text").
                        Count().ShouldBe(1);
                });
            }
        }

        [TestFixture]
        public class When_Description_is_changed : DelegateCommandContext
        {
            [SetUp]
            public void Setup()
            {
                Given(DelegateCommand_is_created);

                When("Description is changed", () =>
                    delegateCommand.Description = "Logon to the system");
            }

            [Test]
            public void assure_Observers_are_notified_about_state_change()
            {
                Then(() =>
                    changeRecorder.Data.Where(r => r.PropertyName == "Description").
                        Count().ShouldBe(1));
            }

        }


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
        public class When_Execute : DelegateCommandContext
        {
            [Test]
            public void assure_it_can_be_called_without_an_argument()
            {
                Given(DelegateCommand_is_created);

                When("Execute", () =>
                    delegateCommand.Execute());

                Then(() => executeDelegateIsCalled.ShouldBeTrue());
            }

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

            [Test]
            public void assure_BeforeExecute_event_is_triggered()
            {
                Given(DelegateCommand_is_created);
                And("observers has subscribed to the BeforeExecute event", () =>
                    delegateCommand.BeforeExecute += (o, e) => beforeExecuteEventIsTriggered = true);

                When("Execute", () =>
                    delegateCommand.Execute());

                Then(() =>
                     beforeExecuteEventIsTriggered.ShouldBeTrue());
            }

            [Test]
            public void assure_AfterExecute_event_is_triggered()
            {
                Given(DelegateCommand_is_created);
                And("observers has subscribed to the AfterExecute event", () =>
                    delegateCommand.AfterExecute += (o, e) => afterExecuteEventIsTriggered = true);

                When("Execute", () =>
                    delegateCommand.Execute());

                Then(() =>
                    afterExecuteEventIsTriggered.ShouldBeTrue());
            }

            [Test]
            public void assure_Before_and_AfterExecute_events_are_fired_in_the_right_order()
            {
                Given("Log is created", () =>
                    log = new List<string>());
                And("DelegateCommand is created", () =>
                {
                    delegateCommand = new DelegateCommand(() => log.Add("Execute"));
                });
                And("Observers has subscribed to Before and AfterExecute events", () =>
                {
                    delegateCommand.BeforeExecute += (o, e) => log.Add("Before");
                    delegateCommand.AfterExecute += (o, e) => log.Add("After");
                });

                When("Execute", () =>
                    delegateCommand.Execute());

                Then(() =>
                {
                    log[0].ShouldBe("Before");
                    log[1].ShouldBe("Execute");
                    log[2].ShouldBe("After");
                });
            }
        }

        [TestFixture]
        public class When_CanExecute : DelegateCommandContext
        {
            [SetUp]
            public void Setup()
            {
                Given(DelegateCommand_is_created);
            }

            [Test]
            public void assure_it_can_be_called_without_an_argument()
            {
                When("CanExecute", () =>
                    canExecuteResult = delegateCommand.CanExecute());

                Then(() => { });
            }
        }

        [TestFixture]
        public class When_set_Execute_delegate : DelegateCommandContext
        {
            [SetUp]
            public void Setup()
            {
                Given(DelegateCommand_is_created);

            }

            [Test]
            public void assure_delegate_is_set()
            {
                When("set Execute delegate", () =>
                    delegateCommand.ExecuteDelegate = () => customExecuteDelegateIsCalled = true);

                Then(() =>
                {
                    delegateCommand.Execute(null);
                    customExecuteDelegateIsCalled.ShouldBeTrue();
                });
            }

            [Test]
            public void assure_value_is_validated()
            {
                When("set Execute delegate");

                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        delegateCommand.ExecuteDelegate = null));
            }
        }

        [TestFixture]
        public class When_set_CanExecute_delegate : DelegateCommandContext
        {
            [SetUp]
            public void Setup()
            {
                Given(DelegateCommand_is_created);
            }

            [Test]
            public void assure_delegate_is_set()
            {
                When("set CanExecute delegate", () =>
                    delegateCommand.CanExecuteDelegate = () =>
                    {
                        customCanExecuteDelegateIsCalled = true;
                        return true;
                    });

                Then(() =>
                {
                    delegateCommand.CanExecute(null);
                    customCanExecuteDelegateIsCalled.ShouldBeTrue();
                });
            }

            [Test]
            public void assure_value_is_validated()
            {
                When("set CanExecute delegate");

                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        delegateCommand.CanExecuteDelegate = null));
            }
        }
    }
}
