using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Specification
{
    public abstract class Specification<T> : ISpecification<T> where T: class 
    {
        public abstract bool IsSatisfiedBy(T val);
        public T AssembleObject()
        {
            T obj = NewObject();

            if (IsSatisfiedBy(obj) == false)
                throw new Exception("Assembled object doesn't match specification");

            return obj;
        }

        protected virtual T NewObject()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
