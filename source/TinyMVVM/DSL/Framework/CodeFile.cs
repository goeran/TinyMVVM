using System;
using System.IO;
using TinyMVVM.DSL.TextParser;

namespace TinyMVVM.DSL.Framework
{
    public class CodeFile : ICodeLoader
    {
        private string filePath;

        public CodeFile(string filePath)
        {
            if(filePath == null)
                throw new ArgumentNullException();

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found", filePath);

            this.filePath = filePath;
        }

        public string Load()
        {
            using (var reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
                reader.Close();
            }
        }
    }
}
