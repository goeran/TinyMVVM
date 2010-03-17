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
	class AutoBindCommandDelegatesSpecs
	{
		[TestFixture]
		public class When_spawned : ConventionsTestContext
		{
			[SetUp]
			public void Setup()
			{
				When(AutoBindCommandDelegates_is_spawned);
			}

			[Test]
			public void assure_its_a_IViewModelConvention()
			{
				Then(() =>
				     (autoBindCommandDelegates is IViewModelConvention).ShouldBeTrue());
			}
		}

		[TestFixture]
		public class When_applied_to_ViewModel : ConventionsTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(LoginViewModel_is_created);
				And(AutoBindCommandDelegates_is_created);

				When("convention is applied to ViewModel", () =>
					autoBindCommandDelegates.ApplyTo(loginViewModel));
			}

			[Test]
			public void assure_ExecuteDelegate_is_set()
			{
				Then(() =>
				{
					loginViewModel.Login.Execute(null);

					loginViewModel.LoginIsExecuted.ShouldBeTrue();
				});
			}
		}
	}
}
