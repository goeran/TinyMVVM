using System.Collections.Generic;
using TinyMVVM.Specifications;

namespace TinyMVVM.Repositories
{
    public interface IRepository<T> where T: class 
    {
    	IEnumerable<T> Get();
        IEnumerable<T> Get(ISpecification<T> spec);
    }
}
