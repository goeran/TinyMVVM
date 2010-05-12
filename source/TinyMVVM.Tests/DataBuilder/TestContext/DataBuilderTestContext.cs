using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DataBuilder;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.Tests.DataBuilder.TestContext
{
    public class DataBuilderTestContext : NUnitScenarioClass
    {
        protected static ObjectBuilder objectBuilder;
        protected static Node recipe;
        protected static Object result;
    	protected static Customer customer;
    	protected static IList<Customer> customers;

        protected Context ObjectBuilder_is_created = () =>
        {
            objectBuilder = new ObjectBuilder();
        };

        protected When build = () =>
        {
            result = objectBuilder.Build(recipe);
        	customer = result as Customer;
        	customers = result as List<Customer>;
        };

        protected class Customer
        {
        	public ObservableCollection<Employee> Employees { get; set; }
            public Employee CEO { get; set; }
        }

        protected class Employee
        {
            
        }
    }
}
