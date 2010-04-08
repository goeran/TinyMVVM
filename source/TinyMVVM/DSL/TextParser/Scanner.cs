using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TinyMVVM.DSL.TextParser
{
    /// <summary>
    /// Lexical analyser
    /// </summary>
    public class Scanner : ILexicalAnalyzer
    {
        public IEnumerable<Token> Scan(string code)
        {
            using (TextReader textReader = new StringReader(code))
            {
                char chr;
                string name;
                Kind keyword;
                while (textReader.Peek() != -1)
                {
                    chr = (char)textReader.Peek();

                    if (chr == '[')
                    {
                        name = ScannAttribute(textReader);
                        yield return Token.Attribute(name);
                    }
                    else if (char.IsLetter(chr))
                    {
                        name = ScanName(textReader);
                        keyword = ConvertToKeyword(name);
                        if (keyword == Kind.Name)
                            yield return Token.Name(name);
                        else
                            yield return Token.Keyword(keyword);
                    }
                    else
                        textReader.Read();
                }

                yield return Token.EOF;
            }
        }

        private string ScannAttribute(TextReader textReader)
        {
            var sb = new StringBuilder();

            Char chr;

            do
            {
                chr = (char) textReader.Read();
                sb.Append(chr);

            } while (chr != ']');

            return sb.ToString();
        }

        private Kind ConvertToKeyword(string name)
        {
            Kind retValue = Kind.Name;
            try
            {
                retValue = (Kind) Enum.Parse(typeof (Kind), name, true);
            } catch
            {
            } 
            return retValue;
        }

        private string ScanName(TextReader textReader)
        {
            var sb = new StringBuilder();

            while (char.IsLetter((char)textReader.Peek()) ||
				textReader.Peek() == '<' ||
				textReader.Peek() == '>' ||
                textReader.Peek() == '[' ||
                textReader.Peek() == ']' ||
                textReader.Peek() == '(' ||
                textReader.Peek() == ')' ||
                textReader.Peek() == '.' ||
                char.IsNumber((char)textReader.Peek()))
                sb.Append((char)textReader.Read());

            return sb.ToString();
        }
    }
}
