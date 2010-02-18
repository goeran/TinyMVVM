using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel;

namespace TinyMVVM.DSL.TextParser
{
    public class Parser
    {
        private ILexicalAnalyzer scanner;
        private ModelSpecification modelSpecification;
        private ViewModel semanticModel;
        private TokenEnumerator tokensEnumerator;
        private string loadedCode;
        private Dictionary<string, Type> typeConversionTable = new Dictionary<string, Type>();

        public Parser() : 
            this(new Scanner())
        {
            
        }

        public Parser(ILexicalAnalyzer scanner)
        {
            if (scanner == null)
                throw new ArgumentNullException("scanner");

            this.scanner = scanner;

            DefinePrimitiveTypeConversion();
        }

        private void DefinePrimitiveTypeConversion()
        {
            typeConversionTable.Add("string", typeof(string));
            typeConversionTable.Add("int", typeof (int));
            typeConversionTable.Add("bool", typeof (bool));
            typeConversionTable.Add("float", typeof (float));
        }

        public ModelSpecification Parse(ICodeLoader loadingStrategy)
        {
            if (loadingStrategy == null)
                throw new ArgumentNullException();

            loadedCode = loadingStrategy.Load();

            if (loadedCode == null)
                throw new InvalidOperationException("Code must be loaded before it's possible to parse");

            tokensEnumerator = new TokenEnumerator(scanner.Scan(loadedCode));

            modelSpecification = new ModelSpecification();

            ParseViewModels();

            return modelSpecification;
        }

        private void ParseViewModels()
        {
            while (CurrentToken() != Token.EOF)
            {
                ParseViewModel();
            }
        }

        private Token NextToken()
        {
            tokensEnumerator.MoveNext();
            return tokensEnumerator.Current;
        }

        private void ParseViewModel()
        {
            ParseViewModelName();
            ParseViewModelBody();
        }

        private void ParseViewModelName()
        {
            var nameToken = NextToken();
            semanticModel = new ViewModel(nameToken.Value);
            modelSpecification.AddViewModel(semanticModel);
        }

        private void ParseViewModelBody()
        {
            while (CurrentToken() != Token.ViewModel &&
                CurrentToken() != Token.EOF)
            {
                var token = tokensEnumerator.Current;
                if (token == Token.Data)
                    ParseViewModelData();
                else if (token == Token.Command)
                    ParseViewModelCommand();

                NextToken();
            }
        }

        private Token CurrentToken()
        {
            return tokensEnumerator.Current;   
        }

        private void ParseViewModelData()
        {
            //Name
            var name = NextToken().Value;

            //is
            NextToken();

            //Type
            var type = NextToken().Value;

            semanticModel.AddViewModelData(
                new ViewModelData(name, typeConversionTable[type.ToLower()]));
        }

        private void ParseViewModelCommand()
        {
            var nameToken = NextToken();
            semanticModel.AddViewModelCommand(
                new ViewModelCommand(nameToken.Value));
        }

        private class TokenEnumerator : IEnumerator<Token>
        {
            private int index = 0;
            private IEnumerable<Token> tokens;

            public TokenEnumerator(IEnumerable<Token> tokens)
            {
                this.tokens = tokens;
            }

            public void Dispose()
            {
                tokens = null;  
            }

            public bool MoveNext()
            {
                if (index + 1 < tokens.Count())
                {
                    index++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                index = 0;
            }

            public Token Current
            {
                get { return tokens.ElementAt(index);} 
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}
