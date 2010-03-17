using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Framework;
using Moq;
using TinyMVVM.Framework.Conventions;

namespace TinyMVVM.Tests.Framework.TestContext
{
    public class ViewModelBaseContext : NUnitScenarioClass
    {
        protected static CustomViewModel viewModel;
    	protected static Mock<IViewModelConvention> conventionMock;

    	protected Context ClassThatImplments_ViewModelBase_is_created = () =>
    	{
    		viewModel = new CustomViewModel();
    	};

    	protected Context ConventionMock_is_created = () =>
    	{
    		conventionMock = new Mock<IViewModelConvention>();
    	};


        protected When ClassThatImplements_ViewModelBase_is_spawned = () =>
        {
            viewModel = new CustomViewModel();
        };

        public class CustomViewModel : ViewModelBase
        {
			public CustomViewModel()
			{
				ApplyDefaultConventions();
			}

        	public ReadOnlyCollection<IViewModelConvention> Conventions
        	{
				get { return AppliedConventions; }
        	}
        }
    }
}
