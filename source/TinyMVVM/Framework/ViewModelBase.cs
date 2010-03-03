using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TinyMVVM.Framework.Internal;

namespace TinyMVVM.Framework
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DataRecorder PropertyChangeRecorder { get; protected set; }

        public ViewModelBase()
        {
            PropertyChangeRecorder = new DataRecorder(this);
        }

        protected void TriggerPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T GetInstance<T>() where T: class
        {
            return ServiceLocator.Instance.GetInstance<T>();
        }
    }
}
