using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework.Conventions
{
	public class SetServiceLocator : IViewModelConvention
	{
		public void ApplyTo(ViewModelBase viewModel)
		{
			var setMethod = typeof (ServiceLocator).GetMethod("SetLocatorIfNotSet", new Type[]{ typeof(IServiceLocator) });
			setMethod.Invoke(viewModel, new object[]
        	{
				ServiceLocator.GetServiceLocator()
			});
		}
	}
}
