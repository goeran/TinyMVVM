using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests.DataBuilder.Internal.TestContext;

namespace TinyMVVM.Tests.DataBuilder.Internal
{
    public class AllNameRepositorySpecs
    {
        [TestFixture]
        public class When_get_all : RepositoryTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given("Repository is created");
            }

            [Test]
            public void assure_a_resultset_is_returned()
            {
                foreach (var repository in repositories)
                {
                    When("get all from " + repository.GetType().Name, () =>
                        result = repository.Get());

                    Then(() =>
                         result.ShouldNotBeNull());
                }
            }

            [Test]
            public void assure_resultset_contains_items()
            {
                foreach (var repository in repositories)
                {
                    When("get all from " + repository.GetType().Name, () =>
                        result = repository.Get());

                    Then(() =>
                         result.Count().ShouldNotBe(0));
                }

            }

        	[Test]
        	public void assure_MaleNameRepository_doesnt_contain_any_femal_names()
        	{
        		When(get_all_names_from_Male_and_Female_Repository);

        		Then(() =>
        		{
        			var foundFemaleNames = new List<string>();

					femaleNames.ForEach(femaleName =>
					{
						if (maleNames.Contains(femaleName))
							foundFemaleNames.Add(femaleName);
					});

					foundFemaleNames.ForEach(n =>  
						Console.WriteLine(n));

					foundFemaleNames.ShouldHave(0);
        		});
        	}

        	[Test]
        	public void assure_FemaleNameRepository_doesnt_contain_any_male_names()
        	{
        		When(get_all_names_from_Male_and_Female_Repository);

				Then(() =>
				{
					var foundMaleNames = new List<string>();

					maleNames.ForEach(maleName =>
					{
						if (femaleNames.Contains(maleName))
							foundMaleNames.Add(maleName);
					});

					foundMaleNames.ForEach(n =>
						Console.WriteLine(n));

					foundMaleNames.ShouldHave(0);
				});
        	}
        }
    }
}
