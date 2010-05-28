using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Properties;

namespace TinyMVVM.DataBuilder.Internal.Repositories
{
	internal class WordRepository : StringResourceRepository
	{
		protected override string StringResource()
		{
			return Resources.Words;
		}
	}
}
