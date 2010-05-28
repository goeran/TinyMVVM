using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DataBuilder;
using TinyMVVM.DataBuilder.Internal;
using TinyMVVM.DataBuilder.Internal.Repositories;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.Tests.DataBuilder.TestContext
{
    public class DataBuilderTestContext : NUnitScenarioClass
    {
        protected static ObjectBuilder objectBuilder;
        protected static Part part;
        protected static Object result;
    	protected static Customer customer;
    	protected static IList<Customer> customers;
    	protected static List<string> names;
    	protected static List<string> femaleNames;
    	protected static List<string> maleNames;

    	protected Context Part_is_created = () =>
    	{
			part = new Part(typeof(Customer));    		
    	};

        protected Context ObjectBuilder_is_created = () =>
        {
        	femaleNames = new FemaleNameRepository().Get().ToList();
        	maleNames = new MaleNameRepository().Get().ToList();
            objectBuilder = new ObjectBuilder();
        };

        protected When build_object_graph = () =>
        {
            result = objectBuilder.Build(part);
        	customer = result as Customer;
        	customers = result as List<Customer>;
        };

        protected class Customer
        {
        	public string Name { get; set; }
        	public ObservableCollection<Employee> Employees { get; set; }
            public Employee CEO { get; set; }
        }

        protected class Employee
        {
        	public string Name { get; set; }
        	public Employee Boss { get; set; }
        }

    	protected static PartMetadata partMetadata;

    	protected When PartMetadata_is_spawned = () =>
    	{
    		partMetadata = new PartMetadata(part);
    	};
    }
}
