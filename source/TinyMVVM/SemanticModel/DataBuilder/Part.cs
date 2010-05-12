using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DataBuilder
{
	public class Part
	{
		private List<Part> parts = new List<Part>();

		public ReadOnlyCollection<Part> Parts
		{
			get
			{
				return new ReadOnlyCollection<Part>(parts);
			}
		}

		public Type Type { get; private set; }
		public Part Parent { get; set; }
	    public string Name { get; set; }
		public Object Value { get; set; }

		public bool IsRoot
		{
			get { return Parent == null; }
		}

		public Part(Type type)
		{
			if (type == null) throw new ArgumentNullException();

			Type = type;
		}

		public static Part NewPropertyPart(string name, Type type)
		{
            if (type == null) throw new ArgumentNullException();
            if (name == null) throw new ArgumentNullException();

			var propertyNode = new PropertyPart(type);
		    propertyNode.Name = name;

			return propertyNode;
		}

		public void AddPart(Part part)
		{
			if (part == null) throw new ArgumentNullException();

            part.Parent = this;
			parts.Add(part);
		}

	    public static Part NewValuePart(Type type)
	    {
            if (type == null) throw new ArgumentNullException();

	        var valueNode = new ValuePart(type);

	        return valueNode;
	    }
	}
}
