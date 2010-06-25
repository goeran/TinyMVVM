using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Templates
{
	public class UnitTestTemplate : ITemplate
	{
		public UnitTestTemplate()
		{
			Content = Resources.UnitTestTemplateContent;
		}

		public string Content { get; private set; }
	}
}
