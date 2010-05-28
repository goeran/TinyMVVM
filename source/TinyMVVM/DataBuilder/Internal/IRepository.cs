using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Specifications;

namespace TinyMVVM.DataBuilder.Internal
{
    internal interface IRepository<T> where T: class 
    {
    	IEnumerable<T> Get();
        IEnumerable<T> Get(ISpecification<T> spec);
    }
}
