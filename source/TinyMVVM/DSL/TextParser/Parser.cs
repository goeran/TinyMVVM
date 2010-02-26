using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public Parser() : 
            this(new Scanner())
        {
            
        }

        public Parser(ILexicalAnalyzer scanner)
        {
            if (scanner == null)
                throw new ArgumentNullException("scanner");

            this.scanner = scanner;
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
            var nameToken = NextToken();
            if (nameToken.Kind != Kind.Name)
                throw new InvalidSyntaxException("Name must be specified when using the 'viewmodel' keyword");

            ParseViewModelName();
            ParseViewModelBody();
        }

        private void ParseViewModelName()
        {
            var nameToken = CurrentToken();
            semanticModel = new ViewModel(nameToken.Value);
            modelSpecification.AddViewModel(semanticModel);
        }

        private void ParseViewModelBody()
        {
            while (CurrentToken() != Token.ViewModel &&
                CurrentToken() != Token.EOF)
            {
                var token = tokensEnumerator.Current;
                if (token == Token.Property ||
                    token == Token.OProperty)
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
            var nameToken = NextToken();
            var asToken = NextToken();
            var typeToken = NextToken();

            if (nameToken.Kind != Kind.Name)
                throw new InvalidSyntaxException("Name must be specified when using the 'property' keyword");

            if (asToken.Kind != Kind.AS ||
                typeToken.Kind != Kind.Name)
                throw new InvalidSyntaxException("Type must be specified when using the 'property' keyword");

            //Name
            var name = nameToken.Value;

            //Type
            var type = typeToken.Value;

            semanticModel.AddViewModelData(
                new ViewModelProperty(name, type));
        }

        private void ParseViewModelCommand()
        {
            var nameToken = NextToken();

            if (nameToken.Kind != Kind.Name)
                throw new InvalidSyntaxException("Name must be specified when using the 'command' keyword");

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
