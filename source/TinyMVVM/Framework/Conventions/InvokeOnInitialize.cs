using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Framework.Internal;

namespace TinyMVVM.Framework.Conventions
{
	public class InvokeOnInitialize : IViewModelConvention
	{
		#region IViewModelConvention Members

		public void ApplyTo(ViewModelBase viewModel)
		{
			var dynamicObject = new DynamicObject(viewModel);

			dynamicObject.InvokeMethod("OnInitialize");
		}

		#endregion
	}
}
