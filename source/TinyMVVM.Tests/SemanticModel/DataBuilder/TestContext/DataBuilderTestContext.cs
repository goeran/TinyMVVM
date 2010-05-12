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
		protected static Part part;
		protected static Part newPart;

		protected Context Part_is_created = () =>
		{
			NewPart();
		};

		protected When Part_is_spawned = () =>
		{
			NewPart();
		};

		private static void NewPart()
		{
			part = new Part(typeof(List<string>));
		}
	}
}
