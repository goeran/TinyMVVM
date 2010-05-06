using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.SemanticModel.MVVM;

namespace TinyMVVM.Tests.SemanticModel.MVVM.TestContext
{
    public class ViewModelContext : NUnitScenarioClass
    {
        public static ViewModel viewModel;

        protected Context ViewModel_is_created = () =>
        {
            viewModel = new ViewModel("LoginViewModel");
        };

        protected When ViewModel_is_spawned = () =>
        {
            viewModel = new ViewModel("LoginViewModel");
        };
    }
}
