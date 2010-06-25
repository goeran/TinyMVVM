using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Templates
{
	public class ViewCodeBehindTemplate : ITemplate
	{
		public ViewCodeBehindTemplate()
		{
			Content = Resources.ViewCodeBehindTemplateContent;
		}

		public string Content { get; private set; }
	}
}
