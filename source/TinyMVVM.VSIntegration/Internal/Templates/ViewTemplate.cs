using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Templates
{
	public class ViewTemplate : ITemplate
	{
		public ViewTemplate()
		{
			Content = Resources.ViewTemplateContent;
		}

		public string Content { get; private set; }
	}
}
