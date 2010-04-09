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
		protected static BindCommandsDelegatesToMethods BindCommandsDelegatesToMethodsConvention;
		protected static InvokeOnInitialize invokeOnInitializeConvention; 
		protected static LoginViewModel viewModel;
        protected static SpecializedLoginViewModel specializedViewModel;

		protected Context BindCommandsExecuteDelegateToMethodConvention_is_created = () =>
		{
			BindCommandsDelegatesToMethodsConvention = new BindCommandsDelegatesToMethods();
		};

		protected Context LoginViewModel_is_created = () =>
		{
			viewModel = new LoginViewModel();
		};

	    protected Context SpecializedLoginViewModel_is_created = () =>
	    {
            specializedViewModel = new SpecializedLoginViewModel();
	        viewModel = specializedViewModel;
	    };

		protected Context InvokeOnInitializeConvention_is_created = () =>
		{
			invokeOnInitializeConvention = new InvokeOnInitialize();
		};


		protected When BindCommandsDelegatesToMethods_is_spawned = () =>
		{
			BindCommandsDelegatesToMethodsConvention = new BindCommandsDelegatesToMethods();
		};

		protected class LoginViewModel : ViewModelBase
		{
			public bool OnInitializedIsExecuted { get; set; }

			public bool LoginIsExecuted { get; private set; }
			public bool LoginCanExecuteIsExecuted { get; private set; }
			public DelegateCommand Login { get; private set; }

			public bool CancelIsExecuted { get; private set; }
			public bool CancelCanExecuteIsExecuted { get; private set; }
			public DelegateCommand Cancel { get; private set; }

			public DelegateCommand Clear { get; private set; }

			public LoginViewModel()
			{
				Login = new DelegateCommand();
				Cancel = new DelegateCommand();
				Clear = new DelegateCommand();
			}

			public void OnInitialize()
			{
				OnInitializedIsExecuted = true;
			}

			private void OnLogin()
			{
				LoginIsExecuted = true;
			}

			public bool CanLogin()
			{
				LoginCanExecuteIsExecuted = true;
				return true;
			}

			protected void OnCancel()
			{
				CancelIsExecuted = true;
			}

			private bool CanCancel()
			{
				CancelCanExecuteIsExecuted = true;
				return true;
			}
		}

        protected class SpecializedLoginViewModel : LoginViewModel
        {
            public bool OnInitializedOnSpecialIsExecuted { get; set; }
            public DelegateCommand ResetPassword { get; private set; }
            public bool ResetPasswordIsExecuted { get; private set; }

            public SpecializedLoginViewModel()
            {
                ResetPassword = new DelegateCommand();
            }

            public void OnInitialize()
            {
                OnInitializedOnSpecialIsExecuted = true;
            }

            private void OnResetPassword()
            {
                ResetPasswordIsExecuted = true;
            }
        }

	}
}
