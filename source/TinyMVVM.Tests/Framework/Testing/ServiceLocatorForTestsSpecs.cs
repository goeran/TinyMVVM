using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework.Testing;
using TinyMVVM.Tests.Framework.Testing.TestContext;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Services.Impl;
using Moq;

namespace TinyMVVM.Tests.Framework.Testing
{
	class ServiceLocatorForTestsSpecs
	{
		[TestFixture]
		public class When_spawned : ServiceLocatorForTestsContext
		{
			[SetUp]
			public void Setup()
			{
				Given(ServiceLocatorForTests_is_created);

				When("spawned");
			}

			[Test]
			public void assure_it_has_mocked_IBackgroundworker()
			{
				Then(() =>
				{
					serviceLocator.GetInstance<IBackgroundWorker>().ShouldNotBeNull();
					serviceLocator.GetInstance<Mock<IBackgroundWorker>>().ShouldNotBeNull();
				});
			}
		}

		[TestFixture]
		public class When_Get_ServiceLocator_for_testing : ServiceLocatorForTestsContext
		{
			[SetUp]
			public void Setup()
			{
				Given("ServiceLocatorForTests is not created");

				When("get ServiceLocator for testing", () =>
					serviceLocator = ServiceLocatorForTesting.GetServiceLocator());
			}

			[Test]
			public void assure_it_returns_ServiceLocator_from_Project()
			{
				Then(() =>
				     serviceLocator.ShouldBeInstanceOfType<CustomServiceLocatorForTesting>());
			}
		}
	}
}
