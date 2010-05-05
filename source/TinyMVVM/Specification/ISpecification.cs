using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Specification
{
	public interface ISpecification<T> where T: class
	{
		bool IsSatisfiedBy(T val);
	}
}
