using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.Tests.SemanticModel.DataBuilder.TestContext
{
	public class DataBuilderTestContext : NUnitScenarioClass
	{
		protected static Node root;

		protected Context Root_is_created = () =>
		{
			NewRoot();
		};

		protected When Root_is_spawned = () =>
		{
			NewRoot();
		};

		private static void NewRoot()
		{
			root = new Node(typeof(List<string>));
		}
	}
}
