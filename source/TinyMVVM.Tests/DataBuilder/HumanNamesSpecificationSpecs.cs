using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DataBuilder;
using TinyMVVM.Tests.DataBuilder.TestContext;

namespace TinyMVVM.Tests.DataBuilder
{
    public class HumanNamesSpecificationSpecs
    {
        [TestFixture]
        public class When_assemble_object : SpecificationsTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(HumanNamesSpecification_is_created);

                When("assemble object", () =>
                    result = humanNamesSpecification.AssembleObject());
            }

            [Test]
            public void assure_a_result_is_returned()
            {
                Then(() =>
                     result.ShouldNotBeNull());
            }

        }
    }

    public class HumanNamesSpecification_Usage_Scenarios 
    {
        [TestFixture]
        public class When_generate_a_list_of_names_with_First_and_Surname : SpecificationsTestContext
        {
            private const int number_of_names = 10;

            [SetUp]
            public void Setup()
            {
                Given(HumanNamesSpecification_is_created);
                And("it's specified to created " + number_of_names + " human names", () =>
                    humanNamesSpecification.Count = number_of_names);
                And("it's specified to include Name", () =>
                    humanNamesSpecification.Options.Add(HumanNameOptions.Name));
                And("it's specified to include Surname", () =>
                    humanNamesSpecification.Options.Add(HumanNameOptions.Surname));

                When("generate a list of names", () =>
                    humanNames = humanNamesSpecification.AssembleObject());
            }

            [Test]
            public void assure_correct_number_of_names_is_created()
            {
                Then(() =>
                {
                    humanNames.Count().ShouldBe(number_of_names);
                });
            }

            [Test]
            public void assure_Firstname_is_set_on_all_names()
            {
                Then(() =>
                {
                    foreach (var humanName in humanNames)
                        humanName.Name.ShouldNotBeNull();
                });
            }

            [Test]
            public void assure_Surname_is_set_on_all_names()
            {
                Then(() =>
                {
                    foreach (var humanName in humanNames)
                        humanName.Surname.ShouldBeNull();
                });
            }

        }

    }
}
