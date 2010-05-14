using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.DataBuilder;
using TinyMVVM.Tests.DataBuilder.TestContext;

namespace TinyMVVM.Tests.SemanticModel.DataBuilder
{
	public class PartMetadataSpecs 
	{
		[TestFixture]
		public class When_spawning : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				When("spawning");
			}

			[Test]
			public void assure_Part_arg_is_validated()
			{
				Then(() =>
					this.ShouldThrowException<ArgumentNullException>(() =>
						new PartMetadata(null)));
			}
		}

		[TestFixture]
		public class When_spawned : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Part_is_created);

				When(PartMetadata_is_spawned);
			}

			[Test]
			public void assure_it_belongs_to_a_Part()
			{
				Then(() => partMetadata.Part.ShouldNotBeNull());
			}

		    [Test]
		    public void assure_it_has_a_Data_bag()
		    {
		        Then(() => partMetadata.Data.ShouldNotBeNull());
		    }

			[Test]
			public void assure_default_Count_is_1()
			{
				Then(() => partMetadata.Count.ShouldBe(1));
			}
		}
	}
}
