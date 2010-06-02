using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework
{
    public class ViewModelException : Exception
    {
        public ViewModelException(string message) : base(message)
        {
            
        }

        public ViewModelException(string message, Exception innerException) :
            base(message, innerException)
        {
            
        }
    }
}
