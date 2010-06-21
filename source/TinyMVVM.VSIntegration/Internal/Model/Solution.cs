using System.Collections.Generic;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    /// <summary>
    /// Aggregated root. Represents a Visual Studio solution.
    /// </summary>
    public class Solution
    {
        internal Solution(string path)
        {
        	Path = path;
            Projects = new List<Project>();
        }

        public virtual string Name { get; set; }
        public virtual List<Project> Projects { get; private set; }
        public virtual string Path { get; set; }

		public virtual Project NewProject(string name)
		{
			var proj = new Project(name);
			proj.DirectoryPath = System.IO.Path.Combine(Path, name);

			Projects.Add(proj);

			return proj;
		}
    }
}
