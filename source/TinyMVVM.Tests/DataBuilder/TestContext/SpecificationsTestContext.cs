using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DataBuilder;
using TinyMVVM.DataBuilder.Internal;

namespace TinyMVVM.Tests.DataBuilder.TestContext
{
    public class SpecificationsTestContext : NUnitScenarioClass
    {
        protected static HumanNamesSpecification humanNamesSpecification;
        protected static IEnumerable<Object> result;
        protected static IEnumerable<HumanName> humanNames;
		protected const int number_of_names = 100;
    	protected static List<string> allMaleNames;
		protected static List<string> allFemaleNames;
		protected static List<string> allSurnames;

        protected Context HumanNamesSpecification_is_created = () =>
        {
            humanNamesSpecification = new HumanNamesSpecification();

        	allMaleNames = new MaleNameRepository().Get().ToList();
        	allFemaleNames = new FemaleNameRepository().Get().ToList();
        	allSurnames = new SurnameRepository().Get().ToList();
        };

		protected AndSemantics its_specified_to_create_a_given_number_of_human_names(int numer)
		{
			return And("it's specified to created " + number_of_names + " human names", () =>
				humanNamesSpecification.Count = number_of_names);
		}
    }
}
