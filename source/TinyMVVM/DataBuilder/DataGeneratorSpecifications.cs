using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Specification;

namespace TinyMVVM.DataBuilder
{
    public class DataGeneratorSpecifications<T> : Specification<IEnumerable<T>> 
    {
        public int Count { get; set; }

        public override bool IsSatisfiedBy(IEnumerable<T> val)
        {
            return true;
        }
    }

    public class HumanNamesSpecification : DataGeneratorSpecifications<HumanName>
    {
        public List<HumanNameOptions> Options { get; set; }

        public HumanNamesSpecification()
        {
            Options = new List<HumanNameOptions>();
        }

        protected override IEnumerable<HumanName> NewObject()
        {
            var newObject = new List<HumanName>();
            for (int i = 0; i < Count; i++)
            {
                newObject.Add(Activator.CreateInstance<HumanName>());
            }

            return newObject;
        }    
    }
}
