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
            Items.Add(newFolder);

            return newFolder;
        }

        public void NewFile(string name)
        {
            if (Files.Where(f => f.Name == name).Count() > 0)
                throw new ArgumentException("File already exists");

            Items.Add(new File() { Name = name });
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
            var subFolder = Folders.Where(f => f.Name == name).SingleOrDefault();

            if (subFolder == null)
                throw new ArgumentException("Folder doesn't exists");

            return subFolder;
        }
    }
}
