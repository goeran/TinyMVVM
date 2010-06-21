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
        internal File(Folder parentFolder)
        {
			if (parentFolder == null) throw new ArgumentNullException();

        	Parent = parentFolder;
        	DirectoryPath = parentFolder.Path;
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

        public virtual void NewCodeBehindFile(string name)
        {
            if (name == null) throw new ArgumentNullException();

        	var newFile = new File(Parent) {Name = name};
        	newFile.DirectoryPath = DirectoryPath;

            Items.Add(newFile);
        }

        public bool HasCodeBehindFile(string name)
        {
            return CodeBehindFiles.Where(f => f.Name == name).Count() > 0;   
        }
    }
}
