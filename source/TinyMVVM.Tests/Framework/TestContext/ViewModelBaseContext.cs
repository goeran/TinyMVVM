using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Framework;

namespace TinyMVVM.Tests.Framework.TestContext
{
    public class ViewModelBaseContext : NUnitScenarioClass
    {
        protected static CustomViewModel viewModel;

        protected When ClassThatImplements_ViewModelBase_is_spawned = () =>
        {
            viewModel = new CustomViewModel();
        };

        public class CustomViewModel : ViewModelBase
        {
        }
    }
}
