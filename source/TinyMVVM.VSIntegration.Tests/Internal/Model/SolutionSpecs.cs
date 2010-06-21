using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Factories;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model
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
