using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

                And("part for list of objects is specified", () =>
                {
                    part = new Part(typeof(List<Customer>));
                	var customer1Part = Part.NewValuePart(typeof (Customer));
                	customer1Part.AddPart(Part.NewPropertyPart("CEO", typeof (Employee)));
                	var employeesPart = Part.NewPropertyPart("Employees", typeof (ObservableCollection<Employee>));
                	var employeePart = Part.NewValuePart(typeof (Employee));
					employeePart.AddPart(Part.NewPropertyPart("Boss", typeof(Employee)));

					employeesPart.AddPart(employeePart);
					customer1Part.AddPart(employeesPart);

					part.AddPart(customer1Part);
					part.AddPart(Part.NewValuePart(typeof(Customer)));
                });

                When(build_object_graph);
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
                    customers.Count.ShouldBe(2);
                });
            }

            [Test]
            public void assure_complex_object_values_are_built()
            {
                Then(() =>
                {
                    customers.First().CEO.ShouldNotBeNull();
                });
            }

        	[Test]
        	public void assure_complex_object_list_properties_are_built()
        	{
        		Then(() =>
        		{
					customers.First().Employees.ShouldNotBeNull();
        		});
        	}

        	[Test]
        	public void assure_complex_object_lists_values_are_built()
        	{
        		Then(() =>
        		{
        			var customer = customers.First();
					customer.Employees.ShouldNotBeNull();
					customer.Employees.First().Boss.ShouldNotBeNull();
        		});
        	}
        }

        [TestFixture]
        public class When_Build_complex_object : DataBuilderTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(ObjectBuilder_is_created);

				And("part for a complex object is specified", () =>
				{
					part = new Part(typeof(Customer));
					part.AddPart(Part.NewPropertyPart("CEO", typeof(Employee)));
					var employeesProperty = Part.NewPropertyPart("Employees", typeof (ObservableCollection<Employee>));
					part.AddPart(employeesProperty);

					employeesProperty.AddPart(Part.NewValuePart(typeof(Employee)));
				});

				When(build_object_graph);
            }

            [Test]
            public void assure_complex_values_are_built()
            {
                Then(() =>
                {
                    result.GetType().ShouldBe(typeof (Customer));
                    var customer = result as Customer;
                    customer.CEO.ShouldNotBeNull();
                });
            }

        	[Test]
        	public void assure_list_properties_are_built()
        	{
        		Then(() =>
        		{
					customer.Employees.ShouldNotBeNull();
        		});
        	}

        	[Test]
        	public void assure_list_properties_values_are_built()
        	{
        		Then(() =>
        		{
					customer.Employees.ShouldHave(1);
        		});
        	}
        }
    }

	public class ObjectBuilder_usage_scenarios
	{
		[TestFixture]
		public class When_building_a_list_of_Customers_of_a_given_size : DataBuilderTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(ObjectBuilder_is_created);

				And("parts for lists of customers are specified", () =>
				{
					part = new Part(typeof(List<Customer>));
					part.Metadata.Count = 10;
				});

				When(build_object_graph);
			}

			[Test]
			public void assure_list_is_created()
			{
				Then(() =>
				{
					var listOfCustomers = result as IList;
					listOfCustomers.ShouldNotBeNull();
					listOfCustomers.ShouldHave(10);
				});
			}
		}
	}
}
