using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Specifications;

namespace TinyMVVM.DataBuilder
{
	public class IsMaleNameSpecification : Specification<HumanName>
	{
		public override bool IsSatisfiedBy(HumanName val)
		{
			if (val.IsMale) return true;

			return false;
		}
	}

	public class IsFemaleSpecification : Specification<HumanName>
	{
		public override bool IsSatisfiedBy(HumanName val)
		{
			if (val.IsFemale) return true;

			return false;
		}
	}
}
