using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework;
using TinyMVVM.Tests.Framework.TestContext;
using Moq;

namespace TinyMVVM.Tests.Framework
{
	class ServiceLocatorSpecs
	{
		[TestFixture]
		public class When_SetLocator : ServiceLocatorContext
		{
			private CustomServiceLocator customServiceLocator = new CustomServiceLocator();

			[Test]
			public void assure_Locator_is_set()
			{
				When("SetLocator", () =>
					ServiceLocator.SetLocator(customServiceLocator));

				Then(() =>
					 ServiceLocator.Instance.ShouldBe(customServiceLocator));
			}

			[Test]
			public void assure_Locator_arg_is_validated()
			{
				When("SetLocator");

				Then(() =>
					this.ShouldThrowException<ArgumentNullException>(() =>
						ServiceLocator.SetLocator(null)));
			}
		}

		[TestFixture]
		public class When_SetLocatorIfNotSet : ServiceLocatorContext
		{
			CustomServiceLocator customServiceLocator = new CustomServiceLocator();

			[SetUp]
			public void Setup()
			{
				Given("ServiceLocator is reset", () =>
					ServiceLocator.Reset());
			}

			[Test]
			public void assure_Locator_is_set()
			{
				When("SetLocatorIfNotSet", () =>
					ServiceLocator.SetLocatorIfNotSet(() => customServiceLocator));
				
				Then(() =>
					ServiceLocator.Instance.ShouldBe(customServiceLocator));
			}

			[Test]
			public void assure_Locator_is_set_only_once()
			{
				When("SetLocatorIfNotSet is called twice", () =>
				{
					ServiceLocator.SetLocatorIfNotSet(() => customServiceLocator);
					ServiceLocator.SetLocatorIfNotSet(() => new CustomServiceLocator());
				});

				Then(() =>
					ServiceLocator.Instance.ShouldBe(customServiceLocator));
			}

			[Test]
			public void assure_arg_is_validated()
			{
				When("SetLocatorIfNotSet");

				Then(() =>
					this.ShouldThrowException<ArgumentNullException>(() =>
						ServiceLocator.SetLocatorIfNotSet(null)));
			}
		}

		[TestFixture]
		public class When_get_ServiceLocator : ServiceLocatorContext
		{
			[SetUp]
			public void Setup()
			{
				Given("ServiceLocator is not created");

				When("get ServiceLocator", () =>
					serviceLocator = ServiceLocator.GetServiceLocator());
			}

			[Test]
			public void assure_it_returns_ServiceLocator_from_Project()
			{
				Then(() =>
				     serviceLocator.ShouldNotBeInstanceOfType<CustomServiceLocator>());
			}
		}

		private class CustomServiceLocator : IServiceLocator
		{
			public T GetInstance<T>() where T : class
			{
				throw new NotImplementedException();
			}
		}
	}
}
