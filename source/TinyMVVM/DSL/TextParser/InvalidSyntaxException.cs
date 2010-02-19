using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DSL.TextParser
{
    public class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException(string message) : 
            base(message)
        {
            
        }
    }
}
