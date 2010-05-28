using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Specifications;
using TinyMVVM.Specifications.DSL;

namespace TinyMVVM.DataBuilder.Internal
{
    public class NameRepository : IRepository<string>
    {
        private FemaleNameRepository femaleNameRepository = new FemaleNameRepository();
        private MaleNameRepository maleNameRepository = new MaleNameRepository();

    	public IEnumerable<string> Get()
    	{
    		return Get(All.ItemsOf<string>());
    	}

    	public IEnumerable<string> Get(ISpecification<string> specification)
        {
            var result = new List<string>();
            result.AddRange(femaleNameRepository.Get());
            result.AddRange(maleNameRepository.Get());

            return result;
        }
    }
}
