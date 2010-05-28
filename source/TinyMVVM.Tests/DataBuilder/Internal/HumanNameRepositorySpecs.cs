using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Dsl.GivenWhenThen;
using TinyBDD.Specification.NUnit;
using TinyMVVM.DataBuilder;
using TinyMVVM.DataBuilder.Internal;
using TinyMVVM.DataBuilder.Internal.Repositories;
using TinyMVVM.DataBuilder.Repositories.DSL;

namespace TinyMVVM.Tests.DataBuilder.Internal
{
	public class HumanNameRepositorySpecs
	{
		[TestFixture]
		public class When_get : Scenario
		{
			[SetUp]
			public void Setup()
			{
				Given(HumanNameRepository_is_created);

				When("get", () =>
					result = humanNameRepository.Get());
			}

			[Test]
			public void assure_resultset_is_returned()
			{
				Then(() => result.ShouldNotBeNull());
			}

			[Test]
			public void assure_resultset_contains_data()
			{
				Then(() => result.Count().ShouldNotBe(0));
			}

			[Test]
			public void assure_resultset_contains_FemaleNames()
			{
				Then(() =>
					result.Where(r => r.IsFemale).Count().ShouldNotBe(0));
			}

			[Test]
			public void assure_resultset_contains_MaleNames()
			{
				Then(() =>
					result.Where(r => r.IsMale).Count().ShouldNotBe(0));
			}

			[Test]
			public void assure_Surname_is_set_on_names()
			{
				Then(() =>
					result.Where(r => r.Surname == null && r.Surname == string.Empty).Count().ShouldBe(0));
			}
		}

		[TestFixture]
		public class When_get_male_names : Scenario
		{
			[SetUp]
			public void Setup()
			{
				Given(HumanNameRepository_is_created);

				When("get male names", () =>
					result = humanNameRepository.Get(All.MaleNames()));
			}

			[Test]
			public void assure_resultset_contains_data()
			{
				Then(() => result.Count().ShouldNotBe(0));
			}

			[Test]
			public void assure_only_male_names_are_returned()
			{
				Then(() =>
					result.Where(r => r.IsFemale).Count().ShouldBe(0));
			}
		}

		[TestFixture]
		public class When_get_female_names : Scenario
		{
			[SetUp]
			public void Setup()
			{
				Given(HumanNameRepository_is_created);

				When("get female names", () =>
					result = humanNameRepository.Get(All.FemaleNames()));
			}

			[Test]
			public void assure_resultset_contains_data()
			{
				Then(() => result.Count().ShouldNotBe(0));
			}

			[Test]
			public void assure_only_female_names_are_returned()
			{
				Then(() =>
					result.Where(r => r.IsMale).Count().ShouldBe(0));
			}
		}
	}

	public class Scenario : NUnitScenarioClass
	{
		protected static HumanNameRepository humanNameRepository;
		protected static IEnumerable<HumanName> result;

		protected Context HumanNameRepository_is_created = () =>
		{
			humanNameRepository = new HumanNameRepository();
		};

	}
}
