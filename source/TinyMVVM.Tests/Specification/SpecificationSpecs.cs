using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests.Specification.TestContext;

namespace TinyMVVM.Tests.Specification
{
    public class SpecificationSpecs
    {
        [TestFixture]
        public class When_AssembleObject : SpecificationTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(IsAdultEmployeeSpecification_is_created);
            }

            [Test]
            public void assure_Object_is_assembled()
            {
                When(assemble_object);

                Then(() => employee.ShouldNotBeNull());
            }


            [Test]
            public void assure_Exception_is_thrown_if_the_created_object_doesnt_match_specification()
            {
                And("and object to be created will not match specification", () =>
                    isAdultEmployeeSpecification.NewInstanceObject = new Employee() { Age = 16 });

                When("assemble object");

                Then(() =>
                {
                    this.ShouldThrowException<Exception>(() =>
                        employeeSpecification.AssembleObject(), ex => 
                            ex.Message.ShouldBe("Assembled object doesn't match specification"));
                });
            }

        }

    }
}
