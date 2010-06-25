using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Templates
{
	public class GeneratedViewModelTemplate : ITemplate
	{
		public GeneratedViewModelTemplate()
		{
			Content = Resources.CodeBehindViewModelTemplateContent;
		}

		public string Content { get; private set; }
	}
}
