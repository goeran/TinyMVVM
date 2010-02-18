using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework
{
    public interface IServiceLocator
    {
        T GetInstance<T>() where T : class;
    }
}
