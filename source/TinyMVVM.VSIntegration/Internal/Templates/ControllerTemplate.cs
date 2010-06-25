using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Templates
{
	public class ControllerTemplate : ITemplate
	{
		public ControllerTemplate()
		{
			Content = Resources.ControllerTemplateContent;
		}

		public string Content { get; private set; }
	}
}
