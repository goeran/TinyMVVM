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
		protected static Node node;

		protected Context Node_is_created = () =>
		{
			NewNode();
		};

		protected When Node_is_spawned = () =>
		{
			NewNode();
		};

		private static void NewNode()
		{
			node = new Node(typeof(List<string>));
		}
	}
}
