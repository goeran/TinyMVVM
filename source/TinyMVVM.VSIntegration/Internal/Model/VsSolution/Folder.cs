using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.VSIntegration.Internal.Model.Internal;

namespace TinyMVVM.VSIntegration.Internal.Model.VsSolution
{
    public class Folder : ProjectItem
    {
		internal Folder(string name, Folder parentFolder)
		{
			Name = name;
			if (parentFolder != null)
			{
				Parent = parentFolder;
				DirectoryPath = System.IO.Path.Combine(parentFolder.DirectoryPath, name);
			}
		}

    	public virtual string CurrentNamespace
    	{
    		get
    		{
    			var sb = new StringBuilder();
    			sb.Append(Project.RootNamespace);
    			sb.Append(".");
    			var folderStack = TreeWalker.GetFolderStack(this);
				while (folderStack.Count > 0)
				{
					sb.Append(folderStack.Pop().Name);

					if (folderStack.Count > 0)
						sb.Append(".");
				}

    			return sb.ToString();
    		}
    	}

		public override string Path
		{
			get
			{
				return DirectoryPath;
			}
		}

	    public IEnumerable<Folder> Folders
        {
            get { return Items.Where(i => i is Folder).Cast<Folder>(); }
        }

        public IEnumerable<File> Files
        {
            get { return Items.Where(i => i is File).Cast<File>(); }
        }

        public virtual Folder NewFolder(string name)
        {
            if (HasFolder(name))
                throw new ArgumentException("Folder already exists");
            ThrowExceptionIfPathIsNotSet();

        	var newFolder = new Folder(name, this);
            Items.Add(newFolder);

            return newFolder;
        }

		public virtual void AddFolder(Folder folder)
		{
			if (folder == null) throw new ArgumentNullException();

			Items.Add(folder);
		}

        private void ThrowExceptionIfPathIsNotSet()
        {
            if (DirectoryPath == null) throw new InvalidOperationException("DirectoryPath must be specified before SubFolder/File can be created");
        }

        public virtual File NewFile(string name)
        {
            if (HasFile(name))
                throw new ArgumentException("File already exists");
            ThrowExceptionIfPathIsNotSet();

            var newFile = new File(this) { Name = name };
            newFile.Parent = this;
            newFile.DirectoryPath = DirectoryPath;
            Items.Add(newFile);

            return newFile;
        }

		public virtual void AddFile(File file)
		{
			if (file == null) throw new ArgumentNullException();

			Items.Add(file);
		}

        public bool HasFolder(string name)
        {
            return Folders.Where(f => f.Name == name).Count() > 0;
        }

        public bool HasFile(string name)
        {
            return Files.Where(f => f.Name == name).Count() > 0;
        }

        public Folder GetSubFolder(string name)
        {
            if (!HasFolder(name))
                throw new ArgumentException("Folder doesn't exists");

            return Folders.Where(f => f.Name == name).SingleOrDefault();
        }

        public File GetFile(string name)
        {
            if (!HasFile(name))
                throw new ArgumentException("File doesn't exists");

            return Files.Where(f => f.Name == name).SingleOrDefault();
        }

    	public virtual File DeleteFile(string name)
    	{
    		var file = GetFile(name);

    		Items.Remove(file);

    		return file;
    	}
    }
}
