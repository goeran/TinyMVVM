using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests.DataBuilder.Internal.TestContext;

namespace TinyMVVM.Tests.DataBuilder.Internal
{
    public class AllNameRepositorySpecs
    {
        [TestFixture]
        public class When_get_all : RepositoryTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given("Repository is created");
            }

            [Test]
            public void assure_a_resultset_is_returned()
            {
                foreach (var repository in repositories)
                {
                    When("get all from " + repository.GetType().Name, () =>
                        result = repository.GetAll());

                    Then(() =>
                         result.ShouldNotBeNull());
                }
            }

            [Test]
            public void assure_resultset_contains_items()
            {
                foreach (var repository in repositories)
                {
                    When("get all from " + repository.GetType().Name, () =>
                        result = repository.GetAll());

                    Then(() =>
                         result.Count().ShouldNotBe(0));
                }

            }

        }

    }
}
