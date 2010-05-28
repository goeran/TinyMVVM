using System;
using System.Collections.Generic;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.DataBuilder.Internal.Factories
{
	internal interface IPartFactory
	{
		void Initialize(ObjectBuilder objectBuilder);
		bool CanCreateObjectsFor(Part value);
		List<Object> CreateObjects(ValuePart part);
		Object CreateObject(Part value);
	}

}
