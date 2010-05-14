using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DataBuilder
{
    public class HumanName
    {
        public string Name { get; set; }
        public string Middlename { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Name != null)
            {
                sb.Append(Name);
                sb.Append(" ");
            }
            else if (Middlename != null)
            {
                sb.Append(Middlename);
                sb.Append(" ");
            }
            else if (Surname != null)
            {
                sb.Append(Surname);
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}
