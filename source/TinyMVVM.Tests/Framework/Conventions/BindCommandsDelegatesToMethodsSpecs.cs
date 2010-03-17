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
					BindCommandsDelegatesToMethodsConvention.ApplyTo(loginViewModel));
			}

			[Test]
			public void assure_Commands_execute_Delegate_is_bound_to_matching_instance_methods()
			{
				Then(() =>
				{
					loginViewModel.Login.Execute(null);
					loginViewModel.LoginIsExecuted.ShouldBeTrue();

					loginViewModel.Cancel.Execute(null);
					loginViewModel.CancelIsExecuted.ShouldBeTrue();
				});
			}

			[Test]
			public void assure_Commands_CanExecute_delegate_is_bound_to_matching_instance_methods()
			{
				Then(() =>
				{
					loginViewModel.Login.CanExecute(null);
					loginViewModel.LoginCanExecuteIsExecuted.ShouldBeTrue();

					loginViewModel.Cancel.CanExecute(null);
					loginViewModel.CancelCanExecuteIsExecuted.ShouldBeTrue();
				});
			}
		}
	}
}
