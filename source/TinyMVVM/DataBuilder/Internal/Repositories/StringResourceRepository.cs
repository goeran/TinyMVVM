using System.Collections.Generic;
using System.IO;
using TinyMVVM.Repositories;
using TinyMVVM.Specifications;
using TinyMVVM.Specifications.DSL;

namespace TinyMVVM.DataBuilder.Internal.Repositories
{
    internal abstract class StringResourceRepository : IRepository<string>
    {
    	public IEnumerable<string> Get()
    	{
    		return Get(All.ItemsOf<string>());
    	}

    	public IEnumerable<string> Get(ISpecification<string> spec)
        {
            var result = new List<string>();
            using (var stream = new StringReader(StringResource()))
            {
                string line = null;
                do
                {
                    line = stream.ReadLine();
                    if (line != null) result.Add(line);
                } while (line != null);
            }

            return result;
        }

        protected abstract string StringResource();
    }
}
