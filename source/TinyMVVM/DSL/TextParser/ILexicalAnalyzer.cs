using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DSL.TextParser
{
    public interface ILexicalAnalyzer
    {
        IEnumerable<Token> Scan(string code);
    }
}
