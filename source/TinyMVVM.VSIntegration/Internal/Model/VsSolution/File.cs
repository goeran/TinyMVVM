using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TinyMVVM.VSIntegration.Internal.Model.VsSolution
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
            if (!System.IO.File.Exists(Path))
                throw new FileNotFoundException();
        }

        public StreamWriter NewFileStream()
        {
            return new StreamWriter(Path, false, Encoding.UTF8);
        }

		public StreamReader OpenFileStream()
		{
			return new StreamReader(Path, Encoding.UTF8);
		}

        public virtual File NewCodeBehindFile(string name)
        {
            if (name == null) throw new ArgumentNullException();

        	var newFile = new File(Parent) {Name = name};
        	newFile.DirectoryPath = DirectoryPath;

            Items.Add(newFile);

        	return newFile;
        }

        public bool HasCodeBehindFile(string name)
        {
            return CodeBehindFiles.Any(f => f.Name == name);   
        }

		public File GetCodeBehindFile(string name)
		{
			if (!HasCodeBehindFile(name))
				throw new ArgumentException("Code Behind File doesn't exist");

			return CodeBehindFiles.Single(f => f.Name == name);
		}

    	public virtual File DeleteCodeBehindFile(string name)
    	{
    		var file = GetCodeBehindFile(name);

    		Items.Remove(file);

    		return file;
    	}

		public virtual void DeleteAllCodeBehindFiles()
		{
			Items.Clear();
		}
    }
}
