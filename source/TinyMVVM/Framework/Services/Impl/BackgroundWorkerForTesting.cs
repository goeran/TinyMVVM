using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework.Services.Impl
{
    public class BackgroundWorkerForTesting : IBackgroundWorker
    {
        #region IBackgroundWorker Members

        public void Invoke(Action a)
        {
            a.Invoke();
        }

        #endregion
    }
}
