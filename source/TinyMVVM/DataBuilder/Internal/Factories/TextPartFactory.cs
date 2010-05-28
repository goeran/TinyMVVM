using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.DataBuilder.Internal.Repositories;
using TinyMVVM.DataBuilder.Repositories.DSL;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.DataBuilder.Internal.Factories
{
	public class TextPartFactory : PartFactory
	{
		private WordRepository wordRepository = new WordRepository();

		public override void Initialize(ObjectBuilder objectBuilder)
		{
		}

		public override bool CanCreateObjectsFor(Part part)
		{
			throw new NotImplementedException();
		}

		public override object CreateObject(Part part)
		{
			var result = new StringBuilder();
			var words = wordRepository.Get();

			int numberOfWordsToCreate = (int)part.Metadata.Data["Text"];

			foreach (var word in words)
			{
				result.Append(word);
				result.Append(" ");
			}

			return result.ToString();
		}
	}
}
