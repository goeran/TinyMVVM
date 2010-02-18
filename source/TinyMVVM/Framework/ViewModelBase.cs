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

        static ViewModelBase()
        {
        }

        protected T GetInstance<T>() where T: class
        {
            return ServiceLocator.Instance.GetInstance<T>();
        }
    }
}
