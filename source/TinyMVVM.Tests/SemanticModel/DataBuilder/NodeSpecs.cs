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

		    [Test]
		    public void assure_it_has_a_Name()
		    {
		        Then(() =>
		             node.Name.ShouldBeNull());
		    }

		}

	    [TestFixture]
	    public class When_creating_new_Value_Node : DataBuilderTestContext
	    {
	        [SetUp]
	        public void Setup()
	        {
	            Given(Node_is_created);

	            When("creating new Value Node");
	        }

	        [Test]
	        public void assure_Type_arg_is_validated()
	        {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        Node.NewValueNode(null)));
	        }

	        [Test]
	        public void assure_created_Node_is_returned()
	        {
                Then(() =>
                    Node.NewValueNode(typeof(string)).ShouldNotBeNull());
	        }

	    }

	    [TestFixture]
	    public class When_new_Value_Node_is_created : DataBuilderTestContext
	    {
	        [SetUp]
	        public void Setup()
	        {
	            Given(Node_is_created);

                When("new Value Node is created", () =>
                    newNode = Node.NewValueNode(typeof(string)));
	        }

	        [Test]
	        public void assure_added_Node_is_a_Value_Node()
	        {
	            Then(() => newNode.GetType().ShouldBe(typeof (ValueNode)));
	        }

			[Test]
			public void assure_its_a_Root_Node()
			{
				//It's a root Node as long it isn't added to another Node
				Then(() => newNode.IsRoot.ShouldBeTrue());
			}
	    }


	    [TestFixture]
	    public class When_creating_new_Property_Node : DataBuilderTestContext
	    {
	        [SetUp]
	        public void Setup()
	        {
	            Given(Node_is_created);

	            When("creating a new Property node");
	        }

	        [Test]
	        public void assure_Type_arg_is_validated()
	        {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        Node.NewPropertyNode(string.Empty, null)));
	        }

	        [Test]
	        public void assure_Name_arg_is_validated()
	        {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        Node.NewPropertyNode(null, typeof(string))));
	        }
	    }


		[TestFixture]
		public class When_new_Property_Node_is_created : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Node_is_created);

				When("created new Property Node", () =>
					newNode = Node.NewPropertyNode("child", typeof(string)));
			}

		    [Test]
		    public void assure_added_node_is_a_Property_Node()
		    {
                Then(() =>
                    newNode.GetType().ShouldBe(typeof(PropertyNode)));
		    }

	    	[Test]
	    	public void assure_its_a_Root_Node()
	    	{
	    		//It's a root Node as long it isn't added to another Node
	    		Then(() => newNode.IsRoot.ShouldBeTrue());
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

		[TestFixture]
		public class When_adding_Node : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Node_is_created);

				When("adding Node");
			}

			[Test]
			public void assure_Node_arg_is_validated()
			{
				Then(() =>
					this.ShouldThrowException<ArgumentNullException>(() =>
						node.AddNode(null)));
			}
		}

		[TestFixture]
		public class When_Node_is_added : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Node_is_created);

				When("Node is added", () =>
					node.AddNode(Node.NewValueNode(typeof(string))));
			}

			[Test]
			public void assure_Node_is_added_to_Nodes()
			{
				Then(() => node.Nodes.ShouldHave(1));
			}

			[Test]
			public void assure_added_Node_is_not_a_Root_Node()
			{
				Then(() => node.Nodes.First().IsRoot.ShouldBeFalse());
			}
		}
	}
}
