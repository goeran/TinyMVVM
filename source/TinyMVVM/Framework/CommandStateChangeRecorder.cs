using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework
{
    public class CommandStateChangeRecorder
    {
        private List<Object> data = new List<object>();

        public ReadOnlyCollection<Object> Data
        {
            get
            {
                return new ReadOnlyCollection<object>(data);
            }
        }

        public CommandStateChangeRecorder(Object viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");
        }
    }
}
