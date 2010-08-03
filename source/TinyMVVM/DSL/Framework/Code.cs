using TinyMVVM.DSL.TextParser;

namespace TinyMVVM.DSL.Framework
{
    public class Code
    {
        public static CodeFile FromFile(string filePath)
        {
            return new CodeFile(filePath);
        }

        public static InlineCode Inline(string code)
        {
            return new InlineCode(code);
        }
    }
}
