using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Templates
{
	public class TestScenarioTemplate : ITemplate
	{
		public TestScenarioTemplate()
		{
			Content = Resources.TestScenarioTemplateContent;
		}

		public string Content { get; private set; }
	}
}
