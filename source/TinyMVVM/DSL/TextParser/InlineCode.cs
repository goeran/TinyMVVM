using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DSL.TextParser
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
