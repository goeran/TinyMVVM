using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class Folder : ProjectItem
    {
        public IEnumerable<Folder> Folders
        {
            get { return Items.Where(i => i is Folder).Cast<Folder>(); }
        }

        public IEnumerable<File> Files
        {
            get { return Items.Where(i => i is File).Cast<File>(); }
        }

        public Folder NewFolder(string name)
        {
            if (HasFolder(name))
                throw new ArgumentException("Folder already exists");
            ThrowExceptionIfPathIsNotSet();

            var newFolder = new Folder() { Name = name };
            newFolder.Parent = this;
            newFolder.Path = System.IO.Path.Combine(Path, name);
            Items.Add(newFolder);

            return newFolder;
        }

        private void ThrowExceptionIfPathIsNotSet()
        {
            if (Path == null) throw new InvalidOperationException("Path must be specified before SubFolder/File can be created");
        }

        public File NewFile(string name)
        {
            if (HasFile(name))
                throw new ArgumentException("File already exists");
            ThrowExceptionIfPathIsNotSet();

            var newFile = new File() { Name = name };
            newFile.Parent = this;
            newFile.Path = System.IO.Path.Combine(Path, name);
            Items.Add(newFile);

            return newFile;
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
    }
}
