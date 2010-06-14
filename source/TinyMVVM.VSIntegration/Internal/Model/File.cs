using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class File : ProjectItem
    {
        public IEnumerable<File> CodeBehindFiles
        {
            get
            {
                return Items.Where(i => i is File).Cast<File>();
            }
        }

        public void NewCodeBehindFile(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException();

            Items.Add(new File() { Name = fileName });
        }
    }
}
