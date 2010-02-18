using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;

namespace TinyMVVM.Tests
{
    public class NUnitScenarioClass : ScenarioClass
    {
        [TearDown]
        public void TearDown()
        {
            StartScenario();   
        }
    }
}
