using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.SemanticModel;

namespace TinyMVVM.Tests.SemanticModel.TestContext
{
    public class ViewModelDataContext : NUnitScenarioClass
    {
        protected static ViewModelProperty viewModelProperty;

        protected When ViewModelData_is_spawned = () =>
        {
            viewModelProperty = new ViewModelProperty("Username", typeof(string).Name, false);
        };
    }
}
