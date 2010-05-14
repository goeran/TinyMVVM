using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.DataBuilder.Internal;

namespace TinyMVVM.Tests.DataBuilder.Internal.TestContext
{
    public class RepositoryTestContext : NUnitScenarioClass
    {
        internal static List<IRepository<string>> repositories = new List<IRepository<string>>()
        {
            new FemaleNameRepository(),
            new MaleNameRepository(),
            new NameRepository(),
            new SurnameRepository()
        };

        internal static FemaleNameRepository femaleNameRepository;
        internal static IEnumerable<string> result;

        protected Context FemaleNameRepository_is_created = () =>
        {
            femaleNameRepository = new FemaleNameRepository();
        };
    }
}
