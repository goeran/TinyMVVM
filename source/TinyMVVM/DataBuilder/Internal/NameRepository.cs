using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DataBuilder.Internal
{
    public class NameRepository : IRepository<string>
    {
        private FemaleNameRepository femaleNameRepository = new FemaleNameRepository();
        private MaleNameRepository maleNameRepository = new MaleNameRepository();

        public IEnumerable<string> GetAll()
        {
            var result = new List<string>();
            result.AddRange(femaleNameRepository.GetAll());
            result.AddRange(maleNameRepository.GetAll());

            return result;
        }
    }
}
