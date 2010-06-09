using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class Folder : ProjectItem
    {
        public Folder()
        {
            Items = new List<ProjectItem>();
        }

        public virtual List<ProjectItem> Items { get; private set; }

        public IEnumerable<File> Files
        {
            get
            {
                var files = Items.Where(i => i is File);
                if (files.Count() > 0)
                    return files.Cast<File>();
                else
                    return new List<File>();
            }
        }
    }
}
