using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;

namespace TinyMVVM.Tests.Learning
{
    [TestFixture]
    public class MEFLearningTests
    {
        private AggregateCatalog aggregateCatalog;
        private CompositionContainer container;

        [SetUp]
        public void Setup()
        {
            aggregateCatalog = new AggregateCatalog();
            container = new CompositionContainer(aggregateCatalog);

            container.ComposeParts(this);
        }

        [Test]
        public void How_to_get_instances_of_exports()
        {
            aggregateCatalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));
            
            var export = container.GetExportedValues<ILog>().First();
            export.ShouldNotBeNull();
            export.ShouldBeInstanceOfType<ConsoleLogger>();
        }

        [Test]
        public void How_to_map_an_specific_export_to_an_contract()
        {
            aggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(TestLogger)));

            var export = container.GetExportedValues<ILog>().First();
            
            export.ShouldBeInstanceOfType<TestLogger>();
        }

        interface ILog
        {
            void Log();
        }

        [Export(typeof(ILog))]
        class ConsoleLogger : ILog
        {
            public void Log()
            {
            }
        }

        [Export (typeof(ILog))]
        class TestLogger : ILog
        {
            public void Log()
            {
            }
        }
    }
}
