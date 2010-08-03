namespace TinyMVVM.DSL.Framework
{
    public class InlineCode : ICodeLoader
    {
        private string code;

        public InlineCode(string code)
        {
            this.code = code;
        }

        public string Load()
        {
            return code;
        }
    }
}
