using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DependencyConfig
{
    public class DependencyBinding
    {
        public DependencyBinding()
        {
            ObjectScope = ObjectScopeEnum.Transient;
        }

        public Type FromType { get; set; }
        public Type ToType { get; set; }
        public Object ToInstance { get; set; }
        public ObjectScopeEnum ObjectScope { get; set; }
    }
}
