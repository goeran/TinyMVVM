using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Framework.Services;

namespace TinyMVVM.Framework.Testing.Services
{
	public class UIInvokerForTesting : IUIInvoker
	{
		public void Invoke(Action a)
		{
			a.Invoke();
		}
	}
}
