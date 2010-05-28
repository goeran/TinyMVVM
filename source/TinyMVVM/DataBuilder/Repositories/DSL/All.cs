using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Specifications;

namespace TinyMVVM.DataBuilder.Repositories.DSL
{
	public class All
	{
		public static IsMaleNameSpecification MaleNames()
		{
			return new IsMaleNameSpecification();
		}

		public static IsFemaleSpecification FemaleNames()
		{
			return new IsFemaleSpecification();
		}

		public static AllSpecification<HumanName> Names()
		{
			return new AllSpecification<HumanName>();
		}
	}
}
