using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DSL.TextParser
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
