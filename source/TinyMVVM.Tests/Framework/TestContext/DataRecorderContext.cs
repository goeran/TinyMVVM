using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Framework;

namespace TinyMVVM.Tests.Framework.TestContext
{
    public class DataRecorderContext : NUnitScenarioClass
    {
        protected static Contact subject;
        protected static DataRecorder dataRecorder;

        protected Context Subject_is_created = () =>
        {
            subject = new Contact();
        };

        protected Context DataRecorder_is_created = () =>
        {
            dataRecorder = new DataRecorder(subject);
        };

        protected When DataRecorder_is_spawned = () =>
        {
            dataRecorder = new DataRecorder(subject);
        };

        protected class Contact : INotifyPropertyChanged
        {
            private int age;

            public int Age
            {
                get { return age; }
                set
                {
                    if (value != age)
                    {
                        age = value;
                        TriggerPropertyChanged("Age");
                    }
                }
            }

            private string name;

            public string Name
            {
                get { return name; }
                set
                {
                    if (value != name)
                    {
                        name = value;
                        TriggerPropertyChanged("Name");
                    }
                }
            }

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            public void TriggerPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
