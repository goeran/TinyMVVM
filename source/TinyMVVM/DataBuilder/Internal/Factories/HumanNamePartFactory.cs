using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.DataBuilder.Internal.Repositories;
using TinyMVVM.DataBuilder.Repositories.DSL;
using TinyMVVM.SemanticModel.DataBuilder;
using TinyMVVM.Utils.ExtensionMethods;

namespace TinyMVVM.DataBuilder.Internal.Factories
{
	internal class HumanNamePartFactory : PartFactory
	{
		private HumanNameRepository humanNameRepository = new HumanNameRepository();

		public override void Initialize(ObjectBuilder objectBuilder)
		{
		}

		public override bool CanCreateObjectsFor(Part part)
		{
			if (part != null &&
				part.Type == typeof(string) &&
				part.Metadata.Data.ContainsKey("HumanName"))
				return true;

			return false;
		}

		public override Object CreateObject(Part part)
		{
			var str = string.Empty;

			if (part.Metadata.Data.ContainsKey("HumanName"))
			{
				var options = (HumanNameOptions)part.Metadata.Data["HumanName"];
				if ((options & HumanNameOptions.FemaleName) == HumanNameOptions.FemaleName)
					str = humanNameRepository.Get(All.FemaleNames()).Random<HumanName>().Name;
				else if ((options & HumanNameOptions.MaleName) == HumanNameOptions.MaleName)
					str = humanNameRepository.Get(All.MaleNames()).Random<HumanName>().Name;
				else if ((options & HumanNameOptions.Name) == HumanNameOptions.Name)
					str = humanNameRepository.Get(All.Names()).Random<HumanName>().Name;

				if ((options & HumanNameOptions.Surname) == HumanNameOptions.Surname)
					str += string.Format(" {0}", humanNameRepository.Get(All.Names()).Random<HumanName>().Surname);
			}

			return str;
		}
	}
}
