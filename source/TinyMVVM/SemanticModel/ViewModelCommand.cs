using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel
{
    public class ViewModelCommand
    {
        public string Name { get; protected set; }

        public ViewModelCommand(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            Name = name;
        }
    }
}
