using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.DataBuilder.Internal.Factories
{
	internal class StringPartFactory : PartFactory
	{
		public override void Initialize(ObjectBuilder objectBuilder)
		{
		}

		public override bool CanCreateObjectsFor(Part part)
		{
			if (part.Type == typeof(string)) return true;
			return false;
		}

		public override object CreateObject(Part part)
		{
			return string.Empty;
		}
	}
}
