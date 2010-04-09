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
			    Given(LoginViewModel_is_created);
				And(InvokeOnInitializeConvention_is_created);

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

	    [TestFixture]
	    public class When_applied_to_ViewModel_that_inherits_from_another_ViewModel : ConventionsTestContext
	    {
	        private SpecializedLoginViewModel specializedViewModel;

	        [SetUp]
	        public void Setup()
	        {
                Given("Specialized LoginViewModel is created", () =>
                {
                    specializedViewModel = new SpecializedLoginViewModel();
                    viewModel = specializedViewModel;
                });

	            And(InvokeOnInitializeConvention_is_created);

                When("convention is applied to specialized ViewModel", () =>
                    invokeOnInitializeConvention.ApplyTo(viewModel));
	        }

	        [Test]
	        public void assure_OnInitialize_is_invoked_on_specialized_ViewModel()
	        {
	            Then(() =>
	            {
	                specializedViewModel.OnInitializedOnSpecialIsExecuted.ShouldBeTrue();
	            });
	        }

	        class SpecializedLoginViewModel : LoginViewModel
            {
                public bool OnInitializedOnSpecialIsExecuted { get; set; }
        
                public void OnInitialize()
                {
                    OnInitializedOnSpecialIsExecuted = true;
                }
            }
	    }


	}
}
