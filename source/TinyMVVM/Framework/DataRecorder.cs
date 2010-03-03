using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TinyMVVM.Framework
{
    public class DataRecorder
    {
        private bool recording_flag;
        private INotifyPropertyChanged subject;

        public Dictionary<string, Object> Data { get; private set; }

        public DataRecorder(INotifyPropertyChanged subject)
        {
            if (subject == null) 
                throw new ArgumentNullException("subject");

            this.subject = subject;
            this.subject.PropertyChanged += subject_PropertyChanged;

            Data = new Dictionary<string, object>();
        }

        void subject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsRecording())
            {
                Data.Add(e.PropertyName, TryGetPropertyValueFromSubject(e.PropertyName));
            }
        }

        private object TryGetPropertyValueFromSubject(string propertyName)
        {
            Object retValue = new CouldNotExtractValueFromProperty();
            var subjectType = subject.GetType();
            var propertyOnSubject = subjectType.GetProperty(propertyName);

            try
            {
                retValue = propertyOnSubject.GetValue(subject, null);
            }
            catch{}

            return retValue;
        }

        public void Start()
        {
            IsRecording(true);
        }

        public void Stop()
        {
            IsRecording(false);
        }

        private bool IsRecording()
        {
            return recording_flag;
        }

        private void IsRecording(bool val)
        {
            recording_flag = val;
        }

        public class CouldNotExtractValueFromProperty
        {
            
        }
    }
}
