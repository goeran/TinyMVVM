#region Copyright
// <copyright>
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or (at your option) any later version.
//  
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//  Lesser General Public License for more details.
//  
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// </copyright> 
// 
// <contactinfo>
//  The project webpage is located at http://tinymvvm.googlecode.com which contains all the  neccessary information. You might also find more information on Gøran's blog:  http://blog.goeran.no.
// </contactinfo>
// 
// <author>Gøran Hansen</author>
// <email>mail@goeran.no</email>
// <date>2010-02-01</date>
// 
#endregion


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TinyMVVM.Framework
{
    public class PropertyChangeRecorder
    {
        private bool recording_flag;
        private INotifyPropertyChanged subject;
        private List<Record> data = new List<Record>();

        public ReadOnlyCollection<Record> Data
        {
            get
            {
                return new ReadOnlyCollection<Record>(data);
            }
        }

        public PropertyChangeRecorder(INotifyPropertyChanged subject)
        {
            if (subject == null) 
                throw new ArgumentNullException("subject");

            this.subject = subject;
            this.subject.PropertyChanged += subject_PropertyChanged;
        }

        void subject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsRecording())
            {
                data.Add(new Record(
                    e.PropertyName,
                    TryGetPropertyValueFromSubject(e.PropertyName)));
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

        public class Record
        {
            public string PropertyName { get; private set; }
            public Object Value { get; private set; }

            internal Record(string propertyName, Object value)
            {
                PropertyName = propertyName;
                Value = value;
            }
        }

        public class CouldNotExtractValueFromProperty
        {
            
        }
    }
}
