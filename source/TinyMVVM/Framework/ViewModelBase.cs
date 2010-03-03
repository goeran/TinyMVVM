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

        public PropertyChangeRecorder PropertyChangeRecorder { get; protected set; }
        public Object CmdStateChangeRecorder { get; protected set; }

        public ViewModelBase()
        {
            PropertyChangeRecorder = new PropertyChangeRecorder(this);
            CmdStateChangeRecorder = new object();
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
