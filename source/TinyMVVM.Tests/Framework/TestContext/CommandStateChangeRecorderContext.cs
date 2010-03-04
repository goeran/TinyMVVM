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
        protected static LoginViewModel viewModel;

        protected Context ViewModel_is_created = () =>
        {
            viewModel = new LoginViewModel();
        };

        protected Context CmdStateChangeRecorder_is_created = () =>
        {
            cmdStateChangeRecorder = new CommandStateChangeRecorder(viewModel);
        };

        protected When CmdStateChangeRecorder_is_spawned = () =>
        {
            cmdStateChangeRecorder = new CommandStateChangeRecorder(viewModel);
        };

        protected class LoginViewModel
        {
            public DelegateCommand LoginCommand { get; private set; }
            public DelegateCommand CancelCommand { get; private set; }

            public LoginViewModel()
            {
                LoginCommand = new DelegateCommand(() => {});
                CancelCommand = new DelegateCommand(() => {});
            }
        }

        protected class WebLoginViewModel : LoginViewModel
        {
            public DelegateCommand ForgotPassword { get; set; }
        }
    }
}
