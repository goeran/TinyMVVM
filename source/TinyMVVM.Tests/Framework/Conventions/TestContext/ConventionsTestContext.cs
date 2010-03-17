using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Framework.Conventions;
using TinyMVVM.Framework;

namespace TinyMVVM.Tests.Framework.Conventions.TestContext
{
	public class ConventionsTestContext : NUnitScenarioClass
	{
		protected static AutoBindCommandDelegates autoBindCommandDelegates;
		protected static LoginViewModel loginViewModel;

		protected Context AutoBindCommandDelegates_is_created = () =>
		{
			autoBindCommandDelegates = new AutoBindCommandDelegates();
		};

		protected Context LoginViewModel_is_created = () =>
		{
			loginViewModel = new LoginViewModel();
		};


		protected When AutoBindCommandDelegates_is_spawned = () =>
		{
			autoBindCommandDelegates = new AutoBindCommandDelegates();
		};

		protected class LoginViewModel : ViewModelBase
		{
			public bool LoginIsExecuted { get; private set; }
			public bool LoginCanExecuteIsExecuted { get; private set; }
			public DelegateCommand Login { get; set; }
			public DelegateCommand Cancel { get; set; }

			public LoginViewModel()
			{
				Login = new DelegateCommand();
				Cancel = new DelegateCommand();
			}

			private void OnLogin()
			{
				LoginIsExecuted = true;
			}
		}
	}
}
