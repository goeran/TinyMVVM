using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;

namespace TinyMVVM.Framework.Services.Impl
{
    [Export (typeof(IUIInvoker))]
    public class UIInvoker : IUIInvoker
    {
        public void Invoke(Action a)
        {
            Application.Current.Dispatcher.Invoke(a);
        }
    }
}
