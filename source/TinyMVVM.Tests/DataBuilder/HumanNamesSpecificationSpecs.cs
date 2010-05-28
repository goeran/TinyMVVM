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
        public class When_assemble_a_list_of_human_names : SpecificationsTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(HumanNamesSpecification_is_created);
            	And(its_specified_to_create_a_given_number_of_human_names(number_of_names));
				And("it's specified to include Name", () =>
					humanNamesSpecification.Options.Add(HumanNameOptions.Name));
				And("it's specified to include Surname", () =>
					humanNamesSpecification.Options.Add(HumanNameOptions.Surname));

                When("assamble a list of human names", () =>
                    humanNames = humanNamesSpecification.AssembleObject());
            }

            [Test]
            public void assure_a_result_is_returned()
            {
                Then(() =>
                     humanNames.ShouldNotBeNull());
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
			public void assure_names_appear_randomly()
			{
				Then(() =>
				{
					var uniqueNames = humanNames.Select(n => n.FullName).Distinct();
					var uniquePercentage = ((double)uniqueNames.Count() / humanNames.Count()) * 100;
					Console.WriteLine(uniquePercentage);
					(uniquePercentage > 50).ShouldBeTrue();
				});
			}
        }

		[TestFixture]
		public class When_assemble_a_list_of_human_names_with_First_and_Surname : SpecificationsTestContext
		{
			[SetUp]
			public void Setup()
			{
				Given(HumanNamesSpecification_is_created);
				And(its_specified_to_create_a_given_number_of_human_names(number_of_names));
				And("it's specified to include Name", () =>
					humanNamesSpecification.Options.Add(HumanNameOptions.Name));
				And("it's specified to include Surname", () =>
					humanNamesSpecification.Options.Add(HumanNameOptions.Surname));

				When("generate a list of names", () =>
					humanNames = humanNamesSpecification.AssembleObject());
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
						humanName.Surname.ShouldNotBeNull();
				});
			}
		}

    	[TestFixture]
    	public class When_assemble_a_list_of_human_names_with_MaleName : SpecificationsTestContext
    	{
    		[SetUp]
    		public void Setup()
    		{
    			Given(HumanNamesSpecification_is_created);
    			And(its_specified_to_create_a_given_number_of_human_names(number_of_names));
				And("it's specified to include MaleName", () =>
					humanNamesSpecification.Options.Add(HumanNameOptions.MaleName));

				When("assemble", () =>
					humanNames = humanNamesSpecification.AssembleObject());
    		}

    		[Test]
    		public void assure_Name_is_set_on_all_names()
    		{
    			Then(() =>
    			{
					humanNames.Where(n => n.Name != null && n.Name != string.Empty).
						Count().ShouldBe(humanNames.Count());
    			});
    		}

    		[Test]
    		public void assure_there_are_no_female_names()
    		{
    			Then(() =>
    			{
    				var names = humanNames.Select(n => n.Name);

    				var numberOfFemalNamesFound = 0;
    				allFemaleNames.ForEach(femaleName =>
    				{
						if (names.Contains(femaleName))
							numberOfFemalNamesFound++;
    				});

					numberOfFemalNamesFound.ShouldBe(0);
    			});
    		}
    	}

    	[TestFixture]
    	public class When_assemble_a_list_of_human_names_with_FemaleName : SpecificationsTestContext
    	{
    		[SetUp]
    		public void Setup()
    		{
    			Given(HumanNamesSpecification_is_created);
    			And(its_specified_to_create_a_given_number_of_human_names(number_of_names));
				And("it's specified to include FemalName", () =>
					humanNamesSpecification.Options.Add(HumanNameOptions.FemaleName));

				When("assemble", () =>
					humanNames = humanNamesSpecification.AssembleObject());
    		}

    		[Test]
    		public void assure_Name_is_set_on_all_names()
    		{
				Then(() =>
				{
					humanNames.Where(n => n.Name != null && n.Name != string.Empty).
						Count().ShouldBe(humanNames.Count());
				});
    		}

    		[Test]
    		public void assure_there_are_no_male_names()
    		{
				Then(() =>
				{
					var names = humanNames.Select(n => n.Name);

					var numberOfMaleNamesFound = 0;
					allMaleNames.ForEach(maleName =>
					{
						if (names.Contains(maleName))
							numberOfMaleNamesFound++;
					});

					numberOfMaleNamesFound.ShouldBe(0);
				});
    		}
    	}
    }
}
