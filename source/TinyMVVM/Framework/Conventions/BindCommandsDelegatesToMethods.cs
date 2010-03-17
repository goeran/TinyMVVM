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
				
				if (dynamicObject.MethodExist(executeMethodName))
				{
					((DelegateCommand)command.GetValue(viewModel, null)).ExecuteDelegate = () =>
					{
						dynamicObject.InvokeMethod(executeMethodName);
					};
				}

				if (dynamicObject.MethodExist(canExecuteMethodName))
				{
					((DelegateCommand) command.GetValue(viewModel, null)).CanExecuteDelegate = () =>
					{
						return (bool) dynamicObject.InvokeMethod(canExecuteMethodName);
					};
				}
			}
		}
	}
}
