using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Properties;

namespace TinyMVVM.DataBuilder.Internal
{
    internal class SurnameRepository : StringResourceRepository
    {
        protected override string StringResource()
        {
            return Resources.Surnames;
        }
    }
}
