using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DataBuilder;

namespace TinyMVVM.Tests.Learning
{
	[TestFixture]
	public class FalgsEnumLearningTests
	{
		[Test]
		public void How_to_combine_values()
		{
			var flags = HumanNameOptions.MaleName | HumanNameOptions.Surname;

			flags.ShouldBe(HumanNameOptions.MaleName | HumanNameOptions.Surname);
		}

		[Test]
		public void How_to_check_if_combined_value_contains_a_specific_value()
		{
			var flags = HumanNameOptions.MaleName | HumanNameOptions.Surname;

			(flags & HumanNameOptions.MaleName).ShouldBe(HumanNameOptions.MaleName);
			(flags & HumanNameOptions.Surname).ShouldBe(HumanNameOptions.Surname);

			var flags2 = HumanNameOptions.Name | HumanNameOptions.Surname;
			(flags2 & HumanNameOptions.FemaleName).ShouldNotBe(HumanNameOptions.FemaleName);
			(flags2 & HumanNameOptions.Name).ShouldBe(HumanNameOptions.Name);
		}
	}
}
