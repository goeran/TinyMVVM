using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.SemanticModel.MVVM;

namespace TinyMVVM.Tests.SemanticModel.MVVM.TestContext
{
    public class ModelSpecificationContext : NUnitScenarioClass
    {
        protected static ModelSpecification modelSpecification;

        protected Context ModelSpecification_is_created = () =>
        {
            modelSpecification = new ModelSpecification();
        };

        protected When spawned = () =>
        {
            modelSpecification = new ModelSpecification();
        };
    }
}
