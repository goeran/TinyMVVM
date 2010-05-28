using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyMVVM.DataBuilder.Internal;
using TinyMVVM.DataBuilder.Internal.Factories;
using TinyMVVM.DataBuilder.Internal.Repositories;
using TinyMVVM.DataBuilder.Repositories.DSL;
using TinyMVVM.SemanticModel.DataBuilder;
using TinyMVVM.Utils.ExtensionMethods;

namespace TinyMVVM.DataBuilder
{
	//TODO: Impl visitor in semantic model => makes it easier to iterate over all parts
    public class ObjectBuilder
    {
		private HumanNameRepository humanNameRepository = new HumanNameRepository();
    	private static readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public |
    	                                                    BindingFlags.NonPublic;

    	private List<PartFactory> listPartValueFactories = new List<PartFactory>();

		public ObjectBuilder()
		{
			listPartValueFactories.Add(new HumanNamePartFactory());
			listPartValueFactories.Add(new DefaultListPartValueFactory());
			listPartValueFactories.Add(new StringPartFactory());
		}

    	public Object Build(Part part)
        {
            Object result = null;

            result = Activator.CreateInstance(part.Type);

            BuildPropertyParts(part, result);

            if (result is IList)
            {
            	BuildValueParts(part, result as IList);
            }

        	return result;
        }

    	private void BuildValueParts(Part listPart, IList list)
    	{
    		var valueParts = listPart.Parts.Where(n => n is ValuePart).Cast<ValuePart>();

			foreach (var valuePart in valueParts)
    		{
    			var valueFactory = FindPartFactory(valuePart);

				if (valueFactory != null)
				{
					valueFactory.Initialize(this);

					foreach (var v in valueFactory.CreateObjects(valuePart))
					{
						list.Add(v);

						if (v is IList)
							BuildValueParts(valuePart, v as IList);
					}
				}
    		}
    	}

		internal PartFactory FindPartFactory(Part part)
		{
			return listPartValueFactories.Where(f => f.CanCreateObjectsFor(part)).FirstOrDefault();			
		}

		internal void BuildPropertyParts(Part part, object result)
        {
            var resultType = result.GetType();
            var propertyParts = part.Parts.Where(n => n is PropertyPart);
            foreach (var propertyPart in propertyParts)
            {
				var prop = resultType.GetProperty(propertyPart.Name,
								  bindingFlags);

            	var partFactory = FindPartFactory(propertyPart);

            	var propValue = partFactory.CreateObject(propertyPart);

				prop.SetValue(
					result,
					propValue,
					null);

				if (propValue != null)
				{
					if (propValue is IList)
						BuildValueParts(propertyPart, propValue as IList);
					else
						BuildPropertyParts(propertyPart, propValue);
				}
            }
        }
    }
}
