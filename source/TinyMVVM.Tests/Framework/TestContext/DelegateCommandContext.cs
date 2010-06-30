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
        protected static PropertyChangeRecorder changeRecorder;
        protected static bool executeDelegateIsCalled;
        protected static bool canExecuteDelegateIsCalled;
    	protected static bool customExecuteDelegateIsCalled;
    	protected static bool customCanExecuteDelegateIsCalled;
        protected static bool canExecuteResult;
        protected static bool beforeExecuteEventIsTriggered;
        protected static bool afterExecuteEventIsTriggered;
        protected static List<string> log;

        protected Context DelegateCommand_is_created = () =>
        {
            beforeExecuteEventIsTriggered = false;
            afterExecuteEventIsTriggered = false;
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

            changeRecorder = new PropertyChangeRecorder(delegateCommand);
            changeRecorder.Start();
        };

        protected When DelegateCommand_is_spawned = () =>
        {
            delegateCommand = new DelegateCommand(() => {});
        };
    }
}
