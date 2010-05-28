using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.DataBuilder.Internal;
using TinyMVVM.Specifications;

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
		private MaleNameRepository maleNameRepository = new MaleNameRepository();
		private FemaleNameRepository femaleNameRepository = new FemaleNameRepository();

        public List<HumanNameOptions> Options { get; set; }

        public HumanNamesSpecification()
        {
            Options = new List<HumanNameOptions>();
        }

        protected override IEnumerable<HumanName> NewObject()
        {
            var names = nameRepository.Get().ToList();
            var surnames = surnameRepository.Get().ToList();
        	var maleNames = maleNameRepository.Get().ToList();
        	var femaleNames = femaleNameRepository.Get().ToList();

            var random = new Random();

            var newObject = new List<HumanName>();
            for (int i = 0; i < Count; i++)
            {
                var humanName = HumanName.NewMaleName();

				if (Options.Contains(HumanNameOptions.Name))
					humanName.Name = names[random.Next(names.Count() - 1)];
				else if (Options.Contains(HumanNameOptions.MaleName))
					humanName.Name = maleNames[random.Next(maleNames.Count() - 1)];
				else if (Options.Contains(HumanNameOptions.FemaleName))
					humanName.Name = femaleNames[random.Next(femaleNames.Count() - 1)];

                humanName.Surname = surnames[random.Next(surnames.Count() - 1)];
                newObject.Add(humanName);
            }

            return newObject;
        }    
    }
}
