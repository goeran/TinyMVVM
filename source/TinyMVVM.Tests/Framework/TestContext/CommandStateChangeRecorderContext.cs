using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Framework;

namespace TinyMVVM.Tests.Framework.TestContext
{
    public class CommandStateChangeRecorderContext : NUnitScenarioClass
    {
        protected static CommandStateChangeRecorder cmdStateChangeRecorder;
        protected static object viewModel;

        protected Context ViewModel_is_created = () =>
        {
            viewModel = new object();
        };

        protected Context CmdStateChangeRecorder_is_created = () =>
        {
            cmdStateChangeRecorder = new CommandStateChangeRecorder(viewModel);
        };

        protected When CmdStateChangeRecorder_is_spawned = () =>
        {
            cmdStateChangeRecorder = new CommandStateChangeRecorder(viewModel);
        };
    }
}
