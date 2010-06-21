namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class Project : Folder
    {
        internal Project(string name) : base(name, null)
        {
            
        }

        public virtual string Type { get; set; }
    }
}
