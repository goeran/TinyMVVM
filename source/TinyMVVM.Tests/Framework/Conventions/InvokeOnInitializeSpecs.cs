using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests.Framework.Conventions.TestContext;

namespace TinyMVVM.Tests.Framework.Conventions
{
	class InvokeOnInitializeSpecs
	{
		[TestFixture]
		public class When_applied_to_ViewModel : ConventionsTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(InvokeOnInitializeConvention_is_created);

				When("convention is applied to ViewModel", () =>
					invokeOnInitializeConvention.ApplyTo(viewModel));
			}

			[Test]
			public void assure_OnIntialize_instance_method_is_invoked()
			{
				Then(() =>
					viewModel.OnInitializedIsExecuted.ShouldBeTrue());
			}
		}
	}
}
