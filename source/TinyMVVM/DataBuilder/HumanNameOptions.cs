using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.DataBuilder
{
	[Flags]
    public enum HumanNameOptions
    {
		Name = 0,
        FemaleName = 1,
        MaleName = 2,
        Surname = 4
    }
}
