using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Framework;

namespace TinyMVVM.Tests.Framework.TestContext
{
    public class DelegateCommandContext : NUnitScenarioClass
    {
        protected static DelegateCommand delegateCommand;
        protected static bool executeDelegateIsCalled;
        protected static bool canExecuteDelegateIsCalled;
    	protected static bool customExecuteDelegateIsCalled;
    	protected static bool customCanExecuteDelegateIsCalled;
        protected static bool canExecuteResult;

        protected Context DelegateCommand_is_created = () =>
        {
            executeDelegateIsCalled = false;
        	customExecuteDelegateIsCalled = false;
            canExecuteDelegateIsCalled = false;
            delegateCommand = new DelegateCommand(() =>
            {
                executeDelegateIsCalled = true;
            }, () =>
            {
                canExecuteDelegateIsCalled = true;
                return true;
            });
        };

        protected When DelegateCommand_is_spawned = () =>
        {
            delegateCommand = new DelegateCommand(() => {});
        };
    }
}
