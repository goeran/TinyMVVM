using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.DataBuilder;
using TinyMVVM.Tests.SemanticModel.DataBuilder.TestContext;

namespace TinyMVVM.Tests.SemanticModel.DataBuilder
{
	public class PartSpecs
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
						new Part(null)));
			}
		}

		[TestFixture]
		public class When_spawned : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				When(Part_is_spawned);
			}

			[Test]
			public void assure_it_has_child_Parts()
			{
				Then(() =>
				    part.Parts.ShouldNotBeNull());
			}

			[Test]
			public void assure_it_has_a_Type()
			{
				Then(() =>
					part.Type.ShouldNotBeNull());
			}

			[Test]
			public void assure_it_has_a_Parent()
			{
				Then(() =>
					part.Parent.ShouldBeNull());
			}

		    [Test]
		    public void assure_it_has_a_Name()
		    {
		        Then(() =>
		             part.Name.ShouldBeNull());
		    }

			[Test]
			public void assure_it_has_a_container_for_Value()
			{
				Then(() => part.Value.ShouldBeNull());
			}

			[Test]
			public void assure_it_has_Metadata()
			{
				Then(() => part.Metadata.ShouldNotBeNull());
			}
		}

	    [TestFixture]
	    public class When_creating_new_Value_Part : DataBuilderTestContext
	    {
	        [SetUp]
	        public void Setup()
	        {
	            Given(Part_is_created);

	            When("creating new Value part");
	        }

	        [Test]
	        public void assure_Type_arg_is_validated()
	        {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        new ValuePart(null)));
	        }

	        [Test]
	        public void assure_created_Part_is_returned()
	        {
                Then(() =>
                    new ValuePart(typeof(string)).ShouldNotBeNull());
	        }

	    }

	    [TestFixture]
	    public class When_new_Value_Part_is_created : DataBuilderTestContext
	    {
	        [SetUp]
	        public void Setup()
	        {
	            Given(Part_is_created);

                When("new Value part is created", () =>
                    newPart = new ValuePart(typeof(string)));
	        }

	        [Test]
	        public void assure_added_Part_is_a_Value_Part()
	        {
	            Then(() => newPart.GetType().ShouldBe(typeof (ValuePart)));
	        }

			[Test]
			public void assure_its_Root()
			{
				//It's a root part as long it isn't added to another part
				Then(() => newPart.IsRoot.ShouldBeTrue());
			}
	    }


	    [TestFixture]
	    public class When_creating_new_Property_Part : DataBuilderTestContext
	    {
	        [SetUp]
	        public void Setup()
	        {
	            Given(Part_is_created);

	            When("creating a new Property part");
	        }

	        [Test]
	        public void assure_Type_arg_is_validated()
	        {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        new PropertyPart(string.Empty, null)));
	        }

	        [Test]
	        public void assure_Name_arg_is_validated()
	        {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        new PropertyPart(null, typeof(string))));
	        }
	    }


		[TestFixture]
		public class When_new_Property_Part_is_created : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Part_is_created);

				When("created new Property part", () =>
					newPart = new PropertyPart("child", typeof(string)));
			}

		    [Test]
		    public void assure_added_Part_is_a_Property_Part()
		    {
                Then(() =>
                    newPart.GetType().ShouldBe(typeof(PropertyPart)));
		    }

	    	[Test]
	    	public void assure_its_Root()
	    	{
	    		//It's a root part as long it isn't added to another part
	    		Then(() => newPart.IsRoot.ShouldBeTrue());
	    	}
		}

		[TestFixture]
		public class When_eval_if_IsRoot : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Part_is_created);
			}

			[Test]
			public void assure_its_a_root_Part_when_it_doesnt_have_a_parent()
			{
				And("it doesn't have a parent", () =>
					part.Parent = null);

				When("eval if IsRoot");

				Then(() =>  
					part.IsRoot.ShouldBeTrue());
			}

			[Test]
			public void assure_its_not_a_root_Part_when_it_have_a_parent()
			{
				And("it does have a parent", () =>
					part.Parent = new Part(typeof(string)));

				When("eval if IsRoot");

				Then(() =>
					part.IsRoot.ShouldBeFalse());
			}
		}

		[TestFixture]
		public class When_adding_Part : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Part_is_created);

				When("adding part");
			}

			[Test]
			public void assure_Part_arg_is_validated()
			{
				Then(() =>
					this.ShouldThrowException<ArgumentNullException>(() =>
						part.AddPart(null)));
			}
		}

		[TestFixture]
		public class When_Part_is_added : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Part_is_created);

				When("part is added", () =>
					part.AddPart(new ValuePart(typeof(string))));
			}

			[Test]
			public void assure_Part_is_added_to_Parts()
			{
				Then(() => part.Parts.ShouldHave(1));
			}

			[Test]
			public void assure_added_Part_is_not_a_Root()
			{
				Then(() => part.Parts.First().IsRoot.ShouldBeFalse());
			}
		}

		[TestFixture]
		public class When_metadata_is_specified : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(Part_is_created);

				When("metadata is specified", () =>
					part.Describe(m =>
					{
						m.Count = 10;
					}));
			}

			[Test]
			public void assure_Metadata_is_changed()
			{
				Then(() =>
				{
					part.Metadata.Count.ShouldBe(10);
				});
			}
		}
	}
}
