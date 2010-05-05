using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.SemanticModel.MVVM;

namespace TinyMVVM.Tests.SemanticModel.TestContext
{
    public class ViewModelPropertyContext : NUnitScenarioClass
    {
        protected static ViewModelProperty viewModelProperty;

		protected GivenSemantics ViewModelProperty_is_created_and_its_a_type_of(string type)
		{
			return Given("ViewModelProperty is created and it's a type of " + type, () =>
				NewViewModelProperty(type));
		}

    	protected Context ViewModelProperty_is_created = () =>
    	{
			NewViewModelProperty(typeof(string).Name);    		
    	};

        protected When ViewModelProperty_is_spawned = () =>
        {
			NewViewModelProperty(typeof(string).Name);
        };

    	protected When eval = () =>
    	{
    		
    	};

    	private static void NewViewModelProperty(string type)
    	{
    		viewModelProperty = new ViewModelProperty("Username", type, false);
    	}
    }
}
