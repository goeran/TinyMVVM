using TestGUI.Tests;
using TinyBDD.Dsl.GivenWhenThen;
namespace TinyMVVM.Tests.Framework.Testing.TestContext
{
	public class ServiceLocatorForTestsContext : NUnitScenarioClass
	{
		protected static ServiceLocatorForTesting serviceLocator;

		protected Context ServiceLocatorForTests_is_created = () =>
		{
			serviceLocator = new ServiceLocatorForTesting();
		};
	}
}
