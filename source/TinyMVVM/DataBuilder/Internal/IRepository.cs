using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DataBuilder.Internal
{
    internal interface IRepository<T> where T: class 
    {
        IEnumerable<T> GetAll();
    }
}
