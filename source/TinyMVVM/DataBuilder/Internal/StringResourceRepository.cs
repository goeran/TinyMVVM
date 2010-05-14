using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyMVVM.Properties;

namespace TinyMVVM.DataBuilder.Internal
{
    internal abstract class StringResourceRepository : IRepository<string>
    {
        public IEnumerable<string> GetAll()
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
