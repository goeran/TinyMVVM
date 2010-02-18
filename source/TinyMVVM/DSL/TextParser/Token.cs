using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DSL.TextParser
{
    public enum Kind
    {
        Name,
        vm,
        data,
        command,
        AS,
        EOF
    }

    public class Token
    {
        private readonly string _value;
        private readonly Kind _kind;

        public string Value
        {
            get { return _value; }
        }

        public Kind Kind
        {
            get { return _kind; }
        }

        private Token()
        {
            
        }

        private Token(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            this._value = value;
        }

        private Token(Kind kind)
        {
            this._kind = kind;
            this._value = string.Empty;
        }

        public static Token ViewModel
        {
            get { return new Token(TextParser.Kind.vm); }
        }

        public static Token Data
        {
            get { return new Token(TextParser.Kind.data); }
        }

        public static Token Command
        {
            get { return new Token(TextParser.Kind.command); }
        }

        public static Token EOF
        {
            get{ return new Token(TextParser.Kind.EOF); }
        }

        public static Token Name(string name)
        {
            return new Token(name);
        }

        public static Token Keyword(Kind kind)
        {
            return new Token(kind);
        }

        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var t = obj as Token;

            return _value.Equals(t._value) && _kind == t.Kind;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Token tokenA, Token tokenB)
        {
            return tokenA.Equals(tokenB); 
        }

        public static bool operator !=(Token tokenA, Token tokenB)
        {
            return !(tokenA == tokenB);
        }

        public override string ToString()
        {
            return string.Format("Token (Kind=" + _kind + ", Value=\"" + _value + "\")");
        }
    }
}
