using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DataBuilder
{
    public class ValueNode : Node
    {
        public ValueNode(Type type) :
            base(type)
        {
            
        }
    }
}
