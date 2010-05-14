using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.DataBuilder.Internal;
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
        private NameRepository nameRepository = new NameRepository();
        private SurnameRepository surnameRepository = new SurnameRepository();
        public List<HumanNameOptions> Options { get; set; }

        public HumanNamesSpecification()
        {
            Options = new List<HumanNameOptions>();
        }

        protected override IEnumerable<HumanName> NewObject()
        {
            var names = nameRepository.GetAll().ToList();
            var surnames = surnameRepository.GetAll().ToList();
            var random = new Random();

            var newObject = new List<HumanName>();
            for (int i = 0; i < Count; i++)
            {
                var humanName = Activator.CreateInstance<HumanName>();
                humanName.Name = names[random.Next(names.Count() - 1)];
                humanName.Surname = surnames[random.Next(surnames.Count() - 1)];
                newObject.Add(humanName);
            }

            return newObject;
        }    
    }
}
