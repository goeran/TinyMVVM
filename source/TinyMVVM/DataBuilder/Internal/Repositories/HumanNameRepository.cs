using System;
using System.Collections.Generic;
using System.Linq;
using TinyMVVM.Repositories;
using TinyMVVM.Specifications;
using TinyMVVM.Specifications.DSL;

namespace TinyMVVM.DataBuilder.Internal.Repositories
{
	public class HumanNameRepository : IRepository<HumanName>
	{
		private NameRepository nameRepository = new NameRepository();
		private FemaleNameRepository femaleNameRepository = new FemaleNameRepository();
		private MaleNameRepository maleNameRepository = new MaleNameRepository();
		private SurnameRepository surnameRepository = new SurnameRepository();
		private List<string> surnames;
		private Random random = new Random();

		public HumanNameRepository()
		{
			surnames = surnameRepository.Get().ToList();
		}

		public IEnumerable<HumanName> Get()
		{
			return Get(All.ItemsOf<HumanName>());
		}

		public IEnumerable<HumanName> Get(ISpecification<HumanName> spec)
		{
			var result = GetAllHumanNames();

			return result.Where(r => spec.IsSatisfiedBy(r));
		}

		private List<HumanName> GetAllHumanNames()
		{
			var result = new List<HumanName>();

			result.AddRange(CreateFemaleNames());
			result.AddRange(CreateMaleNames());

			return result;
		}

		private IEnumerable<HumanName> CreateFemaleNames()
		{
			var result = new List<HumanName>();
			var femaleNames = femaleNameRepository.Get().ToList();

			foreach (var femaleName in femaleNames)
			{
				var humanName = HumanName.NewFemaleName();
				humanName.Name = femaleNames[random.Next(femaleNames.Count() - 1)];
				humanName.Surname = surnames[random.Next(surnames.Count() - 1)];
				result.Add(humanName);
			}

			return result;
		}

		private IEnumerable<HumanName> CreateMaleNames()
		{
			var result = new List<HumanName>();
			var maleNames = maleNameRepository.Get().ToList();

			foreach (var name in maleNames)
			{
				var humanName = HumanName.NewMaleName();
				humanName.Name = maleNames[random.Next(maleNames.Count() - 1)];
				humanName.Surname = surnames[random.Next(surnames.Count() - 1)];
				result.Add(humanName);
			}

			return result;
		}
	}
}
