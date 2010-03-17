using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework.Conventions
{
	public interface IViewModelConvention
	{
		void ApplyTo(ViewModelBase viewModel);
	}
}
