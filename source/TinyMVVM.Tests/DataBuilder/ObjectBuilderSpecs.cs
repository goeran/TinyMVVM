using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.DataBuilder;
using TinyMVVM.Tests.DataBuilder.TestContext;

namespace TinyMVVM.Tests.DataBuilder
{
    public class ObjectBuilderSpecs
    {
        [TestFixture]
        public class When_build_a_list_that_contains_objects : DataBuilderTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(ObjectBuilder_is_created);

                And("recipe for list of object is create", () =>
                {
                    recipe = new Node(typeof(List<Customer>));
                    recipe.NewValueNode(typeof(Customer)).
                        NewPropertyNode("CEO", typeof(Employee));
                    recipe.NewValueNode(typeof(Customer));
                });

                When(build);
            }

            [Test]
            public void assure_list_is_created()
            {
                Then(() => result.GetType().ShouldBe(typeof(List<Customer>)));
            }

            [Test]
            public void assure_values_are_added()
            {
                Then(() =>
                {
                    var list = result as List<Customer>;
                    list.ShouldHave(2);
                });
            }

            [Test]
            public void assure_complex_class_values_are_built_with_values()
            {
                Then(() =>
                {
                    var list = result as List<Customer>;
                    list.First().CEO.ShouldNotBeNull();
                });
            }
            
        }

        [TestFixture]
        public class When_Build_object : DataBuilderTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(ObjectBuilder_is_created);
            }

            [Test]
            public void assure_its_possible_to_build_an_complex_class()
            {
                And("", () =>
                {
                    recipe = new Node(typeof (Customer));
                    recipe.NewPropertyNode("CEO", typeof (Employee));
                });

                When(build);

                Then(() =>
                {
                    result.GetType().ShouldBe(typeof (Customer));
                    var customer = result as Customer;
                    customer.CEO.ShouldNotBeNull();
                });
            }
        }
    }
}
