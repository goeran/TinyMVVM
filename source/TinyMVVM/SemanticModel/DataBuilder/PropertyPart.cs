using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DataBuilder
{
    public class PropertyPart : Part
    {
        public PropertyPart(string name, Type type) : 
            base(type)
        {
			if (name == null) throw new ArgumentNullException();

        	Name = name;
        }
    }
}
