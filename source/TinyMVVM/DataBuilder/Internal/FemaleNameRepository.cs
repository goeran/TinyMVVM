using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyMVVM.Properties;

namespace TinyMVVM.DataBuilder.Internal
{
    internal class FemaleNameRepository : StringResourceRepository
    {
        protected override string StringResource()
        {
            return Resources.FemaleNames;
        }
    }
}
