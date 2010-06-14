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
			private int numberOfWords = 10;

			[SetUp]
			public void Setup()
			{
				Given(TextPartFactory_is_created);
				And("Part is created", () =>
				{
					part = new Part(typeof(string));
					part.Metadata.Data.Add("Text", numberOfWords);
				});

				When(create_object);
			}

			[Test]
			public void assure_object_is_created()
			{
				Then(() => result.ShouldNotBeNull());
			}

			[Test]
            [Ignore("Failing because prod code is not implemented")]
			public void assure_text_contains_correct_number_of_words()
			{
				Then(() => text.Split(' ').Count().ShouldBe(numberOfWords));
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
