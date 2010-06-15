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

        public void NewCodeBehindFile(string name)
        {
            if (name == null) throw new ArgumentNullException();

            Items.Add(new File() { Name = name });
        }

        public bool HasCodeBehindFile(string name)
        {
            return CodeBehindFiles.Where(f => f.Name == name).Count() > 0;   
        }
    }
}
