using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel
{
    public class ViewModelProperty
    {
        public string Name { get; protected set; }
        public Type Type { get; protected set; } 

        public ViewModelProperty(string name, Type type)
        {
            if (name == null || type == null)
                throw new ArgumentNullException();

            Name = name;
            Type = type;
        }
    }
}
