using System;
using System.Collections.Generic;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.DataBuilder.Internal.Factories
{
	public abstract class PartFactory
	{
		public abstract void Initialize(ObjectBuilder objectBuilder);
		public abstract bool CanCreateObjectsFor(Part part);
		public abstract Object CreateObject(Part part);

		public virtual List<Object> CreateObjects(Part part)
		{
			var result = new List<Object>();

			for (int i = 0; i < part.Metadata.Count; i++)
			{
				result.Add(CreateObject(part));
			}

			return result;
		}

	}

}
