using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Framework.Testing;

namespace TinyMVVM.Tests.Framework.Testing.TestContext
{
	public class ServiceLocatorForTestsContext : NUnitScenarioClass
	{
		protected static ServiceLocatorForTesting serviceLocator;

		protected Context ServiceLocatorForTests_is_created = () =>
		{
			serviceLocator = new CustomServiceLocatorForTesting();
		};

		protected class CustomServiceLocatorForTesting : ServiceLocatorForTesting
		{

		}
	}
}
