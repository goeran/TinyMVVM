namespace TinyMVVM.VSIntegration.Internal.Model.VsSolution
{
    public class Project : Folder
    {
        internal Project(string name, Solution solution) : base(name, null)
        {
        	Solution = solution;
        }

		public Solution Solution { get; private set; }
        public virtual string Type { get; set; }
    	public virtual string RootNamespace { get; set; }

		public override string CurrentNamespace
		{
			get
			{
				return RootNamespace;
			}
		}
    }
}
