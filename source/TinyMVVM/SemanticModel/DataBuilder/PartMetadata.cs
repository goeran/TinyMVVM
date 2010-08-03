using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DataBuilder
{
	public class PartMetadata 
	{
		public Part Part { get; private set; }
		
		//Metadata
	    public Dictionary<string, Object> Data { get; private set; }
		public int Count { get; set; }

		public PartMetadata(Part part)
		{
			if (part == null) throw new ArgumentNullException();

			Part = part;
			Data = new Dictionary<string, object>();
			Count = 1;
		}
	}
}
