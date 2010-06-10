using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model.Internal;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class Folder : ProjectItem
    {
        public Folder()
        {
            Items = new List<ProjectItem>();
        }

        public virtual List<ProjectItem> Items { get; private set; }

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

            var newFolder = new Folder() { Name = name };
            newFolder.Parent = this;
            Items.Add(newFolder);

            return newFolder;
        }

        public File NewFile(string name)
        {
            if (HasFile(name))
                throw new ArgumentException("File already exists");

            var newFile = new File() { Name = name };
            newFile.Parent = this;
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
    }
}
