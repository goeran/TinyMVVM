using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Templates;
using TinyMVVM.VSIntegration.Internal.Utils;

namespace TinyMVVM.VSIntegration.Internal.Services
{
	public class CodeGeneratorArgs
	{
		public CodeGeneratorArgs(ModelSpecification modelSpecification, ViewModel viewModel, ITemplate template)
		{
			Guard.Requries<ArgumentException>(modelSpecification != null);
			Guard.Requries<ArgumentException>(viewModel != null);
			Guard.Requries<ArgumentException>(template != null);

			ModelSpecification = modelSpecification;
			ViewModel = viewModel;
			Template = template;
		}

		public ModelSpecification ModelSpecification { get; private set; }
		public ViewModel ViewModel { get; private set; }
		public ITemplate Template { get; private set; }
	}
}
