using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Tests.Internal.Model
{
    public class ProjectSpecs
    {
        [TestFixture]
        public class When_spawned : ModelTestScenario
        {
            [SetUp]
            public void Setup()
            {
                When(Project_is_spawned);
            }

            [Test]
            public void assure_it_has_a_Name()
            {
                Then(() => project.Name.ShouldBeNull());
            }

            [Test]
            public void assure_it_has_Items()
            {
                Then(() => project.Items.ShouldNotBeNull());
            }

            [Test]
            public void assure_it_has_a_Type()
            {
                Then(() => project.Type.ShouldBeNull());
            }
        }
    }

    public class ModelTestScenario : NUnitScenarioClass
    {
        protected static Project project;

        protected When Project_is_spawned = () =>
        {
            project = new Project();
        };
    }
}
