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
    }
}
