using System.Collections.Generic;
using System.Linq;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;

namespace TinyMVVM.VSIntegration.Internal.Model.Internal
{
    internal class TreeWalker
    {
        /// <summary>
        /// Find the project a ProjectItem belongs to.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Project FindProject(ProjectItem item)
        {
            if (item is Project)
                return item as Project;
            else if (item.Parent == null)
                return null;
            else if (item.Parent.GetType() == typeof(Project))
                return item.Parent as Project;
            else
                return FindProject(item.Parent);
        }

		public static File FindFileInSolution(Solution solution, string path)
		{
			foreach (var project in solution.Projects)
			{
				return FindFile(project, path);
			}

			return null;
		}

		private static File FindFile(Folder folder, string path)
		{
			foreach (var file in folder.Files)
			{
				if (file.Path == path) return file;
			}

			foreach (var subFolder in folder.Folders)
			{
				var result = FindFile(subFolder, path);
				if (result != null) return result;
			}

			return null;
		}

		internal static Stack<Folder> GetFolderStack(File file)
		{
			var folderStack = new Stack<Folder>();
			Folder parent = file.Parent;
			while (parent != null && !(parent is Project))
			{
				folderStack.Push(parent);
				parent = parent.Parent;
			}
			return folderStack;
		}

		internal static Stack<Folder> GetFolderStack(Folder folder)
		{
			var folderStack = new Stack<Folder>();
			folderStack.Push(folder);

			Folder parent = folder.Parent;
			while (parent != null && !(parent is Project))
			{
				folderStack.Push(parent);
				parent = parent.Parent;
			}
			return folderStack;
		}
	}
}
