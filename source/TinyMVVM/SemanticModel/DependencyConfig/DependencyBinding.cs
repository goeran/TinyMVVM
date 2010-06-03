using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DependencyConfig
{
    public class DependencyBinding
    {
        public Type FromType { get; set; }
        public Type ToType { get; set; }
        public Object ToInstance { get; set; }
    }
}
