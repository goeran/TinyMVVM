using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework.Services
{
    public interface IUIInvoker
    {
        void Invoke(Action a);
    }
}
