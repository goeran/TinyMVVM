using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework;
using TinyMVVM.Tests.Framework.TestContext;

namespace TinyMVVM.Tests.Framework
{
    public class CommmandStateChangeRecorderSpecs
    {
        [TestFixture]
        public class When_spawning : CommandStateChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                When("spawning");
            }

            [Test]
            public void assure_ViewModel_arg_is_validated()
            {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        new CommandStateChangeRecorder(null)));
            }
        }

        [TestFixture]
        public class When_spawned : CommandStateChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(ViewModel_is_created);

                When(CmdStateChangeRecorder_is_spawned);
            }

            [Test]
            public void assure_it_has_Data()
            {
                Then(() =>
                     cmdStateChangeRecorder.Data.ShouldNotBeNull());
            }
        }

        [TestFixture]
        public class When_recording : CommandStateChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(ViewModel_is_created);
                And(CmdStateChangeRecorder_is_created);
            }
        }
    }
}
