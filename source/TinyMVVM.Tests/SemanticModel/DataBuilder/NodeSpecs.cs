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
				When(Node_is_spawned);
			}

			[Test]
			public void assure_it_has_child_Nodes()
			{
				Then(() =>
				    node.Nodes.ShouldNotBeNull());
			}

			[Test]
			public void assure_it_has_a_Type()
			{
				Then(() =>
					node.Type.ShouldNotBeNull());
			}

			[Test]
			public void assure_it_has_a_Parent()
			{
				Then(() =>
					node.Parent.ShouldBeNull());
			}
		}

		[TestFixture]
		public class When_create_new_child_Node : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Node_is_created);

				When("created new child Node", () =>
					node.NewNode());
			}

			[Test]
			public void assure_child_Node_is_added_to_Nodes()
			{
				Then(() =>
					node.Nodes.ShouldHave(1));
			}
		}

		[TestFixture]
		public class When_eval_if_IsRoot : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Node_is_created);
			}

			[Test]
			public void assure_its_a_root_Node_when_it_doesnt_have_a_parent()
			{
				And("it doesn't have a parent", () =>
					node.Parent = null);

				When("eval if IsRoot");

				Then(() =>
					node.IsRoot.ShouldBeTrue());
			}

			[Test]
			public void assure_its_not_a_root_Node_when_it_have_a_parent()
			{
				And("it does have a parent", () =>
					node.Parent = new Node(typeof(string)));

				When("eval if IsRoot");

				Then(() =>
					node.IsRoot.ShouldBeFalse());
			}
		}
	}
}
