using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.MVVM;

namespace TinyMVVM.Specification.SemanticModel
{
	class ViewModelPropertyIsPrimitiveType : ISpecification<ViewModelProperty>
	{
		private List<Type> primitiveTypes;

		public ViewModelPropertyIsPrimitiveType()
		{
			primitiveTypes = new List<Type>()
			{
				typeof(string),
		        typeof(int),
                typeof(float),
        		typeof(double),
				typeof(byte),
				typeof(short),
				typeof(uint),
				typeof(sbyte),
				typeof(ushort),
				typeof(long),
				typeof(ulong),
				typeof(char),
				typeof(bool)
			};
		}

		public bool IsSatisfiedBy(ViewModelProperty val)
		{
			if (primitiveTypes.Where(t => t.Name.ToLower() == propertyTypeName(val)).Count() > 0)
				return true;

			return false;

		}

		private string propertyTypeName(ViewModelProperty val)
		{
			var name = val.Type.ToLower();

			if (name.Equals("float")) return "single";
			if (name.Equals("int")) return "int32";
			if (name.Equals("short")) return "int16";
			if (name.Equals("uint")) return "uint32";
			if (name.Equals("ushort")) return "uint16";
			if (name.Equals("long")) return "int64";
			if (name.Equals("ulong")) return "uint64";
			if (name.Equals("bool")) return "boolean";

			return name;
		}
	}
}
