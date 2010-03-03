using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework;
using TinyMVVM.Tests.Framework.TestContext;
using System.ComponentModel;

namespace TinyMVVM.Tests.Framework.ViewModelBaseSpecs
{
    [TestFixture]
    public class When_spawned : ViewModelBaseContext
    {
        [SetUp]
        public void Setup()
        {
            When(ClassThatImplements_ViewModelBase_is_spawned);
        }

        [Test]
        public void assure_it_isa_INotifyPropertyChanged()
        {
            Then(() => 
                (viewModel is INotifyPropertyChanged).ShouldBeTrue());
        }

        [Test]
        public void assure_it_has_a_DataRecorder()
        {
            Then(() =>
                 viewModel.Recorder.ShouldNotBeNull());
        }
    }
}
