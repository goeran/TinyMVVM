using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.DataBuilder.Internal.Factories
{
	internal class DefaultListPartValueFactory : PartFactory
	{
		private ObjectBuilder objectBuilder;

		public override void Initialize(ObjectBuilder objectBuilder)
		{
			this.objectBuilder = objectBuilder;
		}

		public override bool CanCreateObjectsFor(Part part)
		{
			if (part.Type == typeof(string)) return false;

			return true;
		}

		public override List<object> CreateObjects(Part part)
		{
			var result = new List<Object>();

			for (int i = 0; i < part.Metadata.Count; i++)
			{
				var obj = Activator.CreateInstance(part.Type);
				objectBuilder.BuildPropertyParts(part, obj);
				result.Add(obj);
			}

			return result;
		}

		public override Object CreateObject(Part part)
		{
			return Activator.CreateInstance(part.Type);		
		}
	}


}
