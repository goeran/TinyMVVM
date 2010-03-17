using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TinyMVVM.Framework.Conventions
{
	public class BindCommandsDelegatesToMethods : IViewModelConvention
	{
		public void ApplyTo(ViewModelBase viewModel)
		{
			var properties = viewModel.GetType().GetProperties();
			var methods = viewModel.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			var commands = properties.Where(p => p.PropertyType == typeof (DelegateCommand));

			foreach (var command in commands)
			{
				var executeMethod = methods.Where(m => m.Name.Equals(string.Format("On{0}", command.Name))).SingleOrDefault();
				var canExecuteMethod = methods.Where(m => m.Name.Equals(string.Format("Can{0}", command.Name))).SingleOrDefault();
				
				if (executeMethod != null)
				{
					((DelegateCommand)command.GetValue(viewModel, null)).ExecuteDelegate = () =>
					{
						executeMethod.Invoke(viewModel, null);
					};
				}

				if (canExecuteMethod != null)
				{
					((DelegateCommand) command.GetValue(viewModel, null)).CanExecuteDelegate = () =>
					{
						return (bool) canExecuteMethod.Invoke(viewModel, null);
					};
				}
			}
		}
	}
}
