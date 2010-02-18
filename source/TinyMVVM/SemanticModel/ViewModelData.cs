using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel
{
    public class ViewModelData
    {
        public string Name { get; protected set; }
        public Type Type { get; protected set; } 

        public ViewModelData(string name, Type type)
        {
            if (name == null || type == null)
                throw new ArgumentNullException();

            Name = name;
            Type = type;
        }
    }
}
