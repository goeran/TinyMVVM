using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Utils.ExtensionMethods
{
	public static class IEnumerableExtensions
	{
		private static Random random = new Random();

		public static T Random<T>(this IEnumerable<T> list)
		{
			var result = list.ToList()[random.Next(list.Count() - 1)];

			return result;
		}
	}
}
