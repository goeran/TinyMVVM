using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Specification;

namespace TinyMVVM.Tests.Specification.TestContext
{
    public class SpecificationTestContext : NUnitScenarioClass
    {
        protected static Specification<Employee> employeeSpecification;
        protected static IsAdultEmployeeSpecification isAdultEmployeeSpecification;
        protected static Employee employee;

        protected Context IsAdultEmployeeSpecification_is_created = () =>
        {
            isAdultEmployeeSpecification = new IsAdultEmployeeSpecification();
            employeeSpecification = isAdultEmployeeSpecification;
        };

        protected When assemble_object = () =>
        {
            employee = employeeSpecification.AssembleObject();
        };


        protected class IsAdultEmployeeSpecification : Specification<Employee>
        {
            public Employee NewInstanceObject { get; set; }

            public IsAdultEmployeeSpecification()
            {
                NewInstanceObject = new Employee()
                {
                    Age = 18
                };
            }

            public override bool IsSatisfiedBy(Employee val)
            {
                return val.Age >= 18;
            }

            protected override Employee NewObject()
            {
                return NewInstanceObject;
            }
        }

        protected class Employee
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        
    }
}
