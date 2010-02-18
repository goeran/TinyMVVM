using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Framework;

namespace TinyMVVM.Framework.Internal
{
    public class ServiceLocatorForTesting : IServiceLocator
    {
        #region IServiceLocator Members

        public T GetInstance<T>() where T: class
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
