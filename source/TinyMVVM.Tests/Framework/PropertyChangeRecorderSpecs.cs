using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework;
using TinyMVVM.Tests.Framework.TestContext;
using System.ComponentModel;

namespace TinyMVVM.Tests.Framework
{
    public class PropertyChangeRecorderSpecs
    {
        [TestFixture]
        public class When_spawning : PropertyChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                When("spawning");
            }

            [Test]
            public void assure_Subject_arg_is_validated()
            {
                Then(() =>
                    this.ShouldThrowException<ArgumentNullException>(() =>
                        new PropertyChangeRecorder(null)));
            }
        }

        [TestFixture]
        public class When_spawned : PropertyChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(Subject_is_created);
            }

            [Test]
            public void assure_it_has_Data()
            {
                When(PropertyChangeRecorder_is_spawned);

                Then(() =>
                    propertyChangeRecorder.Data.ShouldNotBeNull());
            }

            [Test]
            public void assure_it_doesnt_Record_changes()
            {
                And(PropertyChangeRecorder_is_created);
    
                When("Subject is changed", () =>
                    subject.Name = "Gøran");

                Then(() =>
                     propertyChangeRecorder.Data.Count.ShouldBe(0));
            }
        }
            
        [TestFixture]
        public class When_Recording_and_Subject_changes : PropertyChangeRecorderContext
        {
            Dictionary<string, object> changeTable = new Dictionary<string, object>();

            [SetUp]
            public void Setup()
            {
                Given(Subject_is_created);
                And(PropertyChangeRecorder_is_created);
                And("Recording is started", () =>
                    propertyChangeRecorder.Start());
            }

            [Test]
            public void assure_changes_are_recorded()
            {
                When("Subject changes", () =>
                {
                    subject.Name = "Gøran";
                    subject.Age = 28;
                });

                changeTable.Add("Name", "Gøran");
                changeTable.Add("Age", 28);
                foreach (var row in changeTable)
                {
                    Then("assure change in '" + row.Key + "' is recorded", () =>
                    {
                        var record = propertyChangeRecorder.Data.Where(r => r.PropertyName == row.Key).SingleOrDefault();
                        record.ShouldNotBeNull();
                        record.PropertyName.ShouldBe(row.Key);
                        record.Value.ShouldBe(row.Value);
                    });
                }
            }

            [Test]
            public void assure_multiple_changes_in_on_Property_can_be_recorded()
            {
                When("Subject changes", () =>
                {
                    subject.Name = "Gøran";
                    subject.Name = "Gøran Hansen";
                });

                Then(() =>
                {
                    propertyChangeRecorder.Data.Where(r => r.PropertyName == "Name").
                        Count().ShouldBe(2);
                    propertyChangeRecorder.Data[0].Value.ShouldBe("Gøran");
                    propertyChangeRecorder.Data[1].Value.ShouldBe("Gøran Hansen");
                });
            }
        }

        [TestFixture]
        public class When_not_Recording_and_Subject_changes : PropertyChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(Subject_is_created);
                And(PropertyChangeRecorder_is_created);
            }

            [Test]
            public void assure_changes_are_not_recorded()
            {
                When("Subject changes", () =>
                {
                    subject.Name = "Gøran";
                    subject.Age = 28;
                });

                Then(() =>
                     propertyChangeRecorder.Data.Count.ShouldBe(0));
            }

            [Test]
            public void assure_changes_are_not_recorder_after_recording_is_stopped()
            {
                And("Recording is started and Subject changes", () =>
                {
                    propertyChangeRecorder.Start();
                    subject.Name = "Gøran";
                });
                And("Recording is stopped", () =>
                    propertyChangeRecorder.Stop());

                When("Subject change", () =>
                    subject.Age = 28);

                Then(() =>
                     propertyChangeRecorder.Data.Where(r => r.PropertyName == "Age").
                        Count().ShouldBe(0));
            }

        }

        [TestFixture]
        public class When_Subject_changes_and_changed_Property_does_not_exists : PropertyChangeRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(Subject_is_created);
                And(PropertyChangeRecorder_is_created);
                And("Recording is started", () =>
                    propertyChangeRecorder.Start());

                When("Subject change and report about change in a property that does not exist", () =>
                    subject.TriggerPropertyChanged("DoesNotExist"));
            }

            [Test]
            public void assure_changes_are_recorded_but_without_value()
            {
                Then(() =>
                     propertyChangeRecorder.Data.Where(r => r.PropertyName == "DoesNotExist").
                         Count().ShouldBe(1));
            }

            [Test]
            public void assure_value_for_recording_is_a_custom_NullObj()
            {
                Then(() =>
                     propertyChangeRecorder.Data.Where(r => r.PropertyName == "DoesNotExist").
                        Single().Value.ShouldBeInstanceOfType<PropertyChangeRecorder.CouldNotExtractValueFromProperty>());
            }
        }
   }
}
