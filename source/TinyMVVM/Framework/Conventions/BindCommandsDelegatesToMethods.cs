using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TinyMVVM.Framework.Internal;

namespace TinyMVVM.Framework.Conventions
{
	public class BindCommandsDelegatesToMethods : IViewModelConvention
	{
		public void ApplyTo(ViewModelBase viewModel)
		{
			var dynamicObject = new DynamicObject(viewModel);

			var commands = dynamicObject.GetProperties().Where(p => p.PropertyType == typeof (DelegateCommand));

			foreach (var c in commands)
			{
				var command = c;
				var executeMethodName = string.Format("On{0}", command.Name);
				var canExecuteMethodName = string.Format("Can{0}", command.Name);

                var delegateCommand = command.GetValue(viewModel, null) as DelegateCommand;

                if (delegateCommand != null)
                {
                    if (dynamicObject.MethodExist(executeMethodName))
                    {
                        delegateCommand.ExecuteDelegate = () =>
                        {
                            dynamicObject.InvokeMethod(executeMethodName);
                        };
                    }

                    if (dynamicObject.MethodExist(canExecuteMethodName))
                    {
                        delegateCommand.CanExecuteDelegate = () =>
                        {
                            return (bool)dynamicObject.InvokeMethod(canExecuteMethodName);
                        };
                    }
                }
			}
		}
	}
}
