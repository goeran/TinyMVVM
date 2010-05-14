using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DataBuilder;

namespace TinyMVVM.Tests.DataBuilder.TestContext
{
    public class SpecificationsTestContext : NUnitScenarioClass
    {
        protected static HumanNamesSpecification humanNamesSpecification;
        protected static IEnumerable<Object> result;
        protected static IEnumerable<HumanName> humanNames;

        protected Context HumanNamesSpecification_is_created = () =>
        {
            humanNamesSpecification = new HumanNamesSpecification();
        };
    }
}
