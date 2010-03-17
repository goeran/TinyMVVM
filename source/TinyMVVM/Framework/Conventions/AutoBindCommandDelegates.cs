using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TinyMVVM.Framework.Conventions
{
	public class AutoBindCommandDelegates : IViewModelConvention
	{
		public void ApplyTo(ViewModelBase viewModel)
		{
			var properties = viewModel.GetType().GetProperties();

			var commands = properties.Where(p => p.PropertyType == typeof (DelegateCommand));

			foreach (var command in commands)
			{
				//((DelegateCommand)command.GetValue(viewModel, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)).SetExecuteDelegate(); 
			}
		}
	}
}
