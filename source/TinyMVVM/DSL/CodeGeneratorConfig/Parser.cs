using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronRuby.Builtins;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using TinyMVVM.DSL.Framework;
using TinyMVVM.DSL.TextParser;

namespace TinyMVVM.DSL.CodeGeneratorConfig
{
    public class Parser
    {
        private ScriptEngine scriptEngine = IronRuby.Ruby.CreateEngine();

        public HashSet<string> Parse(ICodeLoader loadingStrategy)
        {
            if (loadingStrategy == null) throw new ArgumentNullException();

            var code = string.Format("{0}\n{1}\n@config",
                GenerateConfigDslCode(),
                loadingStrategy.Load());

            Hash result = null;
            try
            {
                result = ParseCode(code);
            }
            catch (MissingMethodException ex)
            {
                throw new InvalidSyntaxException(string.Format("Invalid syntax. {0}", ex.Message), ex);
            }
            catch (SyntaxErrorException ex)
            {
                throw new InvalidSyntaxException(string.Format("INvalid syntax. {0}", ex.Message), ex);
            }

            return ConvertResultToSemanticModel(result);
        }

        private string GenerateConfigDslCode()
        {
            var code = new StringBuilder();
            code.Append("a = \"\"\n");
            code.Append("@config = {} \n");
            code.Append("\n");
            code.Append("def configure \n");
            code.Append("\t yield \n");
            code.Append("end \n");
            code.Append("\n");
            code.Append("def generate(what)\n");
            code.Append("\t @config[what] = true \n");
            code.Append("end \n");

            return code.ToString();
        }

        private Hash ParseCode(string code)
        {
            Hash result = null;

            result = scriptEngine.CreateScriptSourceFromString(code).Compile().Execute<Hash>();

            return result;
        }

        private HashSet<string> ConvertResultToSemanticModel(Hash result)
        {
            var semanticModel = new HashSet<string>();

            if (result != null)
            {
                foreach (var item in result)
                {
                    semanticModel.Add(item.Key.ToString());
                }
            }

            return semanticModel;
        }
    }
}
