using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DataBuilder.Internal.Factories;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.Tests.DataBuilder.Internal.Factories
{
	public class TextPartFactorySpecs 
	{
		[TestFixture]
		public class When_CreateObject : Scenario
		{
			[SetUp]
			public void Setup()
			{
				Given(TextPartFactory_is_created);
				And("Part is created", () =>
				{
					part = new Part(typeof(string));
					part.Metadata.Data.Add("Text", null);
				});

				When(create_object);
			}

			[Test]
			public void assure_object_is_created()
			{
				Then(() => result.ShouldNotBeNull());
			}

			[Test]
			public void assure_text_is_not_empty()
			{
				Then(() => text.Length.ShouldNotBe(0));
			}
		}
	}

	public class Scenario : NUnitScenarioClass
	{
		protected static TextPartFactory factory;
		protected static Part part;
		protected static Object result;
		protected static string text;

		protected Context TextPartFactory_is_created = () =>
		{
			factory = new TextPartFactory();
		};

		protected When create_object = () =>
		{
			result = factory.CreateObject(part);
			text = result as string;
		};

	}
}
