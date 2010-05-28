using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DataBuilder
{
    public class HumanName
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    	public bool IsMale { get; private set; }
    	public bool IsFemale { get; private set; }

		private HumanName()
		{
			
		}

		public static HumanName NewFemaleName()
		{
			return new HumanName()
			{
				IsFemale = true	
			};
		}

		public static HumanName NewMaleName()
		{
			return new HumanName()
			{
				IsMale = true
			};
		}

    	public string FullName
    	{
    		get
    		{
				var sb = new StringBuilder();
				if (Name != null)
				{
					sb.Append(Name);
					sb.Append(" ");
				}
				if (Surname != null)
				{
					sb.Append(Surname);
					sb.Append(" ");
				}

				return sb.ToString();
    		}
    	}

        public override string ToString()
        {
        	return FullName;
        }
    }
}
