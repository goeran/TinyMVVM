using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TinyMVVM.Framework.Services;

namespace TinyMVVM.Framework.Services.Impl
{
    [Export(typeof(IUIInvoker))]
    public class UIInvoker : IUIInvoker
    {
        public void Invoke(Action a)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => a.Invoke());
        }
    }
}
