using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TinyMVVM.DSL.TextParser
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
            }
        }
    }
}
