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
    public class DataRecorderSpecs
    {
        [TestFixture]
        public class When_spawning : DataRecorderContext
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
                        new DataRecorder(null)));
            }
        }

        [TestFixture]
        public class When_spawned : DataRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(Subject_is_created);
            }

            [Test]
            public void assure_it_has_Data()
            {
                When(DataRecorder_is_spawned);

                Then(() =>
                    dataRecorder.Data.ShouldNotBeNull());
            }

            [Test]
            public void assure_it_doesnt_Record_changes()
            {
                And(DataRecorder_is_created);
    
                When("Subject is changed", () =>
                    subject.Name = "Gøran");

                Then(() =>
                     dataRecorder.Data.Count.ShouldBe(0));
            }
        }
            
        [TestFixture]
        public class When_Recording_and_Subject_changes : DataRecorderContext
        {
            Dictionary<string, object> changeTable = new Dictionary<string, object>();

            [SetUp]
            public void Setup()
            {
                Given(Subject_is_created);
                And(DataRecorder_is_created);
                And("Recording is started", () =>
                    dataRecorder.Start());

                When("Subject changes", () =>
                {
                    subject.Name = "Gøran";
                    subject.Age = 28;
                });
            }

            [Test]
            public void assure_changes_are_recorded()
            {
                changeTable.Add("Name", "Gøran");
                changeTable.Add("Age", 28);

                foreach (var row in changeTable)
                {
                    Then("assure change in '" + row.Key + "' is recorded", () =>
                    {
                        dataRecorder.Data.ContainsKey(row.Key).ShouldBeTrue();
                        dataRecorder.Data[row.Key].ShouldBe(row.Value);
                    });
                }
            }
        }

        [TestFixture]
        public class When_not_Recording_and_Subject_changes : DataRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(Subject_is_created);
                And(DataRecorder_is_created);
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
                     dataRecorder.Data.Count.ShouldBe(0));
            }

            [Test]
            public void assure_changes_are_not_recorder_after_recording_is_stopped()
            {
                And("Recording is started and Subject changes", () =>
                {
                    dataRecorder.Start();
                    subject.Name = "Gøran";
                });
                And("Recording is stopped", () =>
                    dataRecorder.Stop());

                When("Subject change", () =>
                    subject.Age = 28);

                Then(() =>
                     dataRecorder.Data.ContainsKey("Age").ShouldBeFalse());
            }

        }

        [TestFixture]
        public class When_Subject_changes_and_changed_Property_does_not_exists : DataRecorderContext
        {
            [SetUp]
            public void Setup()
            {
                Given(Subject_is_created);
                And(DataRecorder_is_created);
                And("Recording is started", () =>
                    dataRecorder.Start());

                When("Subject change and report about change in a property that does not exist", () =>
                    subject.TriggerPropertyChanged("DoesNotExist"));
            }

            [Test]
            public void assure_changes_are_recorded_but_without_value()
            {
                Then(() =>
                     dataRecorder.Data.ContainsKey("DoesNotExist").ShouldBeTrue());
            }

            [Test]
            public void assure_value_for_recording_is_a_custom_NullObj()
            {
                Then(() =>
                     dataRecorder.Data["DoesNotExist"].ShouldBeInstanceOfType<DataRecorder.CouldNotExtractValueFromProperty>());
            }
        }
    }
}
