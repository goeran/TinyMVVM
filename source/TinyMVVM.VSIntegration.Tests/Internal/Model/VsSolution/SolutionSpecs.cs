using System;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model.VsSolution
{
    public class SolutionSpecs
    {
        [TestFixture]
        public class When_spawned : SolutionTestScenario
        {
            [SetUp]
            public void Setup()
            {
                When(Solution_is_spawned);
            }

            [Test]
            public void assure_it_has_a_Name()
            {
                Then(() => solution.Name.ShouldBeNull());
            }

            [Test]
            public void assure_it_has_Projects()
            {
                Then(() => solution.Projects.ShouldNotBeNull());
            }
        }        
    }

    public class SolutionTestScenario : NUnitScenarioClass
    {
        protected static ModelFactory factory = new ModelFactory();
        protected static Solution solution;

        protected When Solution_is_spawned = () =>
        {
            solution = factory.NewSolution(System.IO.Path.Combine(Environment.CurrentDirectory, "Test.sln"));
        };
    }
}
