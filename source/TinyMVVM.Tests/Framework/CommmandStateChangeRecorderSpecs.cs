using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework;
using TinyMVVM.Tests.Framework.TestContext;

namespace TinyMVVM.Tests.Framework
{
    public class CommmandStateChangeRecorderSpecs
    {
        [TestFixture]
        public class When_spawning : CommandStateChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                When("spawning");
            }

            [Test]
            public void assure_ViewModel_arg_is_validated()
            {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        new CommandStateChangeRecorder(null)));
            }
        }

        [TestFixture]
        public class When_spawned : CommandStateChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(ViewModel_is_created);

                When(CmdStateChangeRecorder_is_spawned);
            }

            [Test]
            public void assure_it_has_Data()
            {
                Then(() =>
                     cmdStateChangeRecorder.Data.ShouldNotBeNull());
            }
        }

        [TestFixture]
        public class When_a_Command_in_ViewModel_change_state : CommandStateChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(ViewModel_is_created);
                And(CmdStateChangeRecorder_is_created);
                And("recording", () =>
                    cmdStateChangeRecorder.Start());

            }

            [Test]
            public void assure_state_change_has_been_Recorded()
            {
                When("a Command in ViewModel change state", () =>
                    viewModel.CancelCommand.TriggerCanExecuteChanged());

                Then(() =>
                {
                    cmdStateChangeRecorder.Data.ShouldHave(1);
                    cmdStateChangeRecorder.Data[0].Command.ShouldBe(viewModel.CancelCommand);
                    cmdStateChangeRecorder.Data[0].CanExecute.ShouldBe(true);
                });
            }

            [Test]
            public void assure_multiple_state_change_will_be_recorded()
            {
                int times = 10;

                When("a Command in ViewModel change state multiple times", () =>
                {
                    for (int i = 0; i < times; i++)
                        viewModel.CancelCommand.TriggerCanExecuteChanged();
                });

                Then(() =>
                {
                    cmdStateChangeRecorder.Data.ShouldHave(times);
                    for (int i = 0; i < times; i++)
                        cmdStateChangeRecorder.Data[i].Command.ShouldBe(viewModel.CancelCommand);
                });
            }

            [Test]
            public void assure_state_change_in_different_commands_has_been_recorded()
            {
                When("all Commands in ViewModel change state", () =>
                {
                    viewModel.CancelCommand.TriggerCanExecuteChanged();
                    viewModel.LoginCommand.TriggerCanExecuteChanged();
                });

                Then(() =>
                {
                    cmdStateChangeRecorder.Data.ShouldHave(2);
                    cmdStateChangeRecorder.Data[0].Command.ShouldBe(viewModel.CancelCommand);
                    cmdStateChangeRecorder.Data[1].Command.ShouldBe(viewModel.LoginCommand);
                });
            }
        }

        [TestFixture]
        public class When_a_Command_in_ViewMOdel_is_a_NullObj : CommandStateChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given("ViewModel is created", () =>
                    viewModel = new WebLoginViewModel());
                And(CmdStateChangeRecorder_is_created);

                When("recording", () =>
                    cmdStateChangeRecorder.Start());
            }

            [Test]
            public void assure_it_records_changes_in_Commands_thats_not_a_NullObj()
            {
                Then(() =>
                {
                    viewModel.CancelCommand.TriggerCanExecuteChanged();
                    viewModel.LoginCommand.TriggerCanExecuteChanged();

                    cmdStateChangeRecorder.Data.ShouldHave(2);
                });
            }
        }
    }
}
