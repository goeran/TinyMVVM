using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework.Conventions;
using TinyMVVM.Tests.Framework.Conventions.TestContext;

namespace TinyMVVM.Tests.Framework.Conventions
{
	class BindCommandsDelegatesToMethodsSpecs
	{
		[TestFixture]
		public class When_spawned : ConventionsTestContext
		{
			[SetUp]
			public void Setup()
			{
				When(BindCommandsDelegatesToMethods_is_spawned);
			}

			[Test]
			public void assure_its_a_IViewModelConvention()
			{
				Then(() =>
				     (BindCommandsDelegatesToMethodsConvention is IViewModelConvention).ShouldBeTrue());
			}
		}

		[TestFixture]
		public class When_applied_to_ViewModel : ConventionsTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(LoginViewModel_is_created);	
				And(BindCommandsExecuteDelegateToMethodConvention_is_created);

				When("convention is applied to ViewModel", () =>
					BindCommandsDelegatesToMethodsConvention.ApplyTo(viewModel));
			}

			[Test]
			public void assure_Commands_execute_Delegate_is_bound_to_matching_instance_methods()
			{
				Then(() =>
				{
					viewModel.Login.Execute(null);
					viewModel.LoginIsExecuted.ShouldBeTrue();

					viewModel.Cancel.Execute(null);
					viewModel.CancelIsExecuted.ShouldBeTrue();
				});
			}

			[Test]
			public void assure_Commands_CanExecute_delegate_is_bound_to_matching_instance_methods()
			{
				Then(() =>
				{
					viewModel.Login.CanExecute(null);
					viewModel.LoginCanExecuteIsExecuted.ShouldBeTrue();

					viewModel.Cancel.CanExecute(null);
					viewModel.CancelCanExecuteIsExecuted.ShouldBeTrue();
				});
			}
		}

	    [TestFixture]
	    public class When_applied_to_ViewModel_that_inherits_from_another_ViewModel : ConventionsTestContext
	    {
	        [SetUp]
	        public void Setup()
	        {
	            Given(SpecializedLoginViewModel_is_created);
	            And(BindCommandsExecuteDelegateToMethodConvention_is_created);

                When("convention is applied to ViewModel", () =>
                    BindCommandsDelegatesToMethodsConvention.ApplyTo(specializedViewModel));
	        }

            [Test]
            public void assure_Commands_execute_Delegate_is_bound_to_matching_instance_methods()
            {
                Then(() =>
                {
                    viewModel.Login.Execute(null);
                    viewModel.LoginIsExecuted.ShouldBeTrue();
                });
            }

	        [Test]
	        public void assure_Commands_execute_Delegate_is_bound_on_specialized_ViewModel()
	        {
	            Then(() =>
	            {
                    specializedViewModel.ResetPassword.Execute(null);
                    specializedViewModel.ResetPasswordIsExecuted.ShouldBeTrue();
	            });
	        }

	    }

	}
}
