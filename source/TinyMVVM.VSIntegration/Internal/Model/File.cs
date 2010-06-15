using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using IO = System.IO;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class File : ProjectItem
    {
        internal File()
        {
            
        }

        public IEnumerable<File> CodeBehindFiles
        {
            get
            {
                return Items.Where(i => i is File).Cast<File>();
            }
        }

        public string Content
        {
            get
            {
                ThrowExceptionIfFileNotFound();

                using (var sr = new StreamReader(Path))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private void ThrowExceptionIfFileNotFound()
        {
            if (!IO.File.Exists(Path))
                throw new FileNotFoundException();
        }

        public StreamWriter NewFileStream()
        {
            return new StreamWriter(Path);
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
