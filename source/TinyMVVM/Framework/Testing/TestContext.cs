using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;

namespace TinyMVVM.Framework.Testing
{
    public abstract class TestContext
    {
        protected Mock<T> GetFakeFor<T>() where T : class
        {
            return ServiceLocator.Instance.GetInstance<Mock<T>>();
        }

    }
}
