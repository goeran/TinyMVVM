using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Utils
{
	public class Guard
	{
		public static void Requries<T>(bool booleanExpression) where T : Exception
		{
			if (booleanExpression == false) throw default(T);
		}
	}
}
