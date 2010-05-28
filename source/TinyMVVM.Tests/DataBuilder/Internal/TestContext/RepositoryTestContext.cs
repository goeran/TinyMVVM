using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DataBuilder.Internal;
using TinyMVVM.DataBuilder.Internal.Repositories;
using TinyMVVM.Repositories;

namespace TinyMVVM.Tests.DataBuilder.Internal.TestContext
{
    public class RepositoryTestContext : NUnitScenarioClass
    {
		internal static FemaleNameRepository femaleNameRepository = new FemaleNameRepository();
		internal static MaleNameRepository maleRepository = new MaleNameRepository();

        internal static List<IRepository<string>> repositories = new List<IRepository<string>>()
        {
			femaleNameRepository,
			maleRepository,
            new NameRepository(),
            new SurnameRepository()
        };

        internal static IEnumerable<string> result;
    	internal static List<string> femaleNames;
		internal static List<string> maleNames;

    	protected When get_all_names_from_Male_and_Female_Repository = () =>
    	{
			maleNames = maleRepository.Get().ToList();
			femaleNames = femaleNameRepository.Get().ToList();
    	};
    }
}
