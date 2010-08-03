using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IronRuby.Builtins;
using Microsoft.Scripting.Hosting;
using NUnit.Framework;

namespace TinyMVVM.VSIntegration.Tests.Learning
{
    [TestFixture]
    public class HostIronRubyTests
    {
        private ScriptEngine scriptEngine;
        private ScriptScope scope;
    	private string scriptPath;

    	[SetUp]
        public void Setup()
        {
            scriptEngine = IronRuby.Ruby.CreateEngine();
            scope = scriptEngine.CreateScope();
        }

        [Test]
        public void How_to_host_the_runtime()
        {
            var script = scriptEngine.CreateScriptSourceFromString("def haldis()\n \t \nend\n { \"build views?\" => false }").Compile();

            var dynamic = script.Execute();

            Console.WriteLine(dynamic.ToString());
        }

        [Test]
        [Ignore("Don't know how to get access to the variable in script")]
        public void How_to_manipulate_a_parameter_in_script()
        {
            scope.SetVariable("aVar", 1);
            var script = scriptEngine.CreateScriptSourceFromString("GetVariable(\"aVar\")");

            var dynamic = script.Execute();

            Console.WriteLine(dynamic.ToString());
        }

        [Test]
        public void How_to_load_script_from_file()
        {
        	scriptPath = Path.Combine(Environment.CurrentDirectory, "Learning", "Script", "config.rb");

        	var script = scriptEngine.CreateScriptSourceFromFile(scriptPath).Compile();

            var config = script.Execute() as Hash;

            Console.WriteLine("output: {0}", config);

            if (config is Hash)
            {
                foreach (var key in config.Keys)
                {
                    Console.WriteLine("{0} = {1}", key, config[key]);
                }
            }
        }

    	[Test]
    	public void How_to_share_object_between_host_and_script()
    	{
    		scriptPath = Path.Combine(Environment.CurrentDirectory, "Learning", "Script", "config2.rb");

			scope.SetVariable("config", new CodeGeneratorConfig());

    		var script = scriptEngine.CreateScriptSourceFromFile(scriptPath);
    		
			var config = script.Execute(scope) as Hash;

            
    	    foreach (var row in config)
    	    {
    	        Console.WriteLine("{0} = {1}", row.Key, row.Value);
    	    }

			Assert.AreEqual(true, config[ToMutableString("views")]);
			Assert.AreEqual(true, config[ToMutableString("controllers")]);
			Assert.AreEqual(true, config[ToMutableString("partial ViewModels")]);
            Assert.AreEqual(false, config[ToMutableString("unit tests")]);
    	}

        private static MutableString ToMutableString(string value)
        {
            return new MutableString().Append(value);
        }

		public class CodeGeneratorConfig
		{
			public bool GenerateViews { get; set; }
			public bool GenerateControllers { get; set; }
			public bool GenerateUnitTests { get; set; }
			public bool GeneratePartialViewModels { get; set; }
		}

        [Test]
        public void How_blocks_works_in_Ruby()
        {
            var scriptPath = Path.Combine(Environment.CurrentDirectory, "Learning", "Script", "blocks.rb");
            var script = scriptEngine.CreateScriptSourceFromFile(scriptPath);

            var result = script.Execute(scope) as RubyArray;

            Assert.IsNotNull(result);

            foreach (var row in result)
            {
                Console.WriteLine("{0}", row);
            }

            Assert.AreEqual("configure", result[0].ToString());
            Assert.AreEqual("generate", result[1].ToString());
            
            
            //Assert.AreEqual("not", result[2].ToString());
            //Assert.AreEqual("not", result[4].ToString());
            //Assert.AreEqual("generate", result[5].ToString());
        }
    }
}
