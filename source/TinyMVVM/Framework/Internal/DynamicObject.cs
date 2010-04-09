using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TinyMVVM.Framework.Internal
{
	internal class DynamicObject
	{
		private BindingFlags instanceFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
		private readonly object instance;
		private readonly Type instanceType;

		public DynamicObject(Object instance)
		{
			this.instance = instance;
			this.instanceType = instance.GetType();
		}

		public bool MethodExist(string name)
		{
			var methods = instanceType.GetMethods(instanceFlags);
			return methods.Where(m => m.Name.Equals(name)).Count() > 0;
		}

		public Object InvokeMethod(string name)
		{
			var method = GetMethod(name);

			if (method != null)
				return method.Invoke(instance, null);
			else
				return null;
		}

		private MethodInfo GetMethod(string name)
		{
			var methods = instanceType.GetMethods(instanceFlags);
			return methods.Where(m => m.Name.Equals(name) && m.DeclaringType == instanceType).SingleOrDefault();
		}

		public MethodInfo[] GetMethods()
		{
			return instanceType.GetMethods(instanceFlags);
		}

		public PropertyInfo[] GetProperties()
		{
			return instanceType.GetProperties(instanceFlags);
		}

	}
}
