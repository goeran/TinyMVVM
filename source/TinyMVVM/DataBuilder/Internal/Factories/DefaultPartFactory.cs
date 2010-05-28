using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.DataBuilder.Internal.Factories
{
	internal class DefaultListPartValueFactory : IPartFactory
	{
		private ObjectBuilder objectBuilder;

		public void Initialize(ObjectBuilder objectBuilder)
		{
			this.objectBuilder = objectBuilder;
		}

		public bool CanCreateObjectsFor(Part value)
		{
			if (value.Type == typeof(string)) return false;

			return true;
		}

		public List<object> CreateObjects(ValuePart part)
		{
			var result = new List<Object>();

			for (int i = 0; i < part.Metadata.Count; i++)
			{
				var obj = Activator.CreateInstance(part.Type);
				objectBuilder.BuildProperties(part, obj);
				result.Add(obj);
			}

			return result;
		}

		public Object CreateObject(Part part)
		{
			return Activator.CreateInstance(part.Type);		
		}
	}


}
