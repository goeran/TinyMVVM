using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Templates
{
	public class PartialViewModelTemplate : ITemplate
	{
		public PartialViewModelTemplate()
		{
			Content = Resources.PartialViewModelTemplateContent;
		}

		public string Content { get; private set; }
	}
}
