using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework.Services.Impl
{
    [Export (typeof(IBackgroundWorker))]
    public class BackgroundWorker : IBackgroundWorker
    {
        #region IBackgroundWorker Members

        public void Invoke(Action a)
        {
            a();
        }

        #endregion
    }
}
