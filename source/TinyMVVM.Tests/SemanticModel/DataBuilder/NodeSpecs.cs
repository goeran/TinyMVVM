using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.DataBuilder;
using TinyMVVM.Tests.SemanticModel.DataBuilder.TestContext;

namespace TinyMVVM.Tests.SemanticModel.DataBuilder
{
	public class NodeSpecs
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
			public void assure_Type_arg_is_validated()
			{
				Then(() =>
					this.ShouldThrowException<ArgumentNullException>(() =>
						new Node(null)));
			}
		}

		[TestFixture]
		public class When_spawned : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				When(Root_is_spawned);
			}

			[Test]
			public void assure_it_has_leafs()
			{
				Then(() =>
				    root.Nodes.ShouldNotBeNull());
			}

			[Test]
			public void assure_it_has_a_Type()
			{
				Then(() =>
					root.Type.ShouldNotBeNull());
			}
		}

		[TestFixture]
		public class When_create_new_child_Node : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Root_is_created);

				When("created new child Node", () =>
					root.NewNode());
			}

			[Test]
			public void assure_child_Node_is_added_to_Nodes()
			{
				Then(() =>
					root.Nodes.ShouldHave(1));
			}
		}
	}
}
