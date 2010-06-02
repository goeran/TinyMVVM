using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DependencyConfig
{
    public class DependencyBinding
    {
        public Type From { get; set; }
        public Type To { get; set; }
    }
}
