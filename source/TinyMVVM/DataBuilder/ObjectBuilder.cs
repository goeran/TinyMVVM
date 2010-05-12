using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.DataBuilder
{
	//TODO: Impl visitor in semantic model => makes it easier to iterate over all parts
    public class ObjectBuilder
    {
    	private static readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public |
    	                                                    BindingFlags.NonPublic;

    	public Object Build(Part part)
        {
            Object result = null;

            result = Activator.CreateInstance(part.Type);

            BuildProperties(part, result);

            if (result is IList)
            {
            	BuildValuesForList(part, result as IList);
            }

        	return result;
        }

    	private void BuildValuesForList(Part part, IList list)
    	{
    		var values = part.Parts.Where(n => n is ValuePart);

			if (part.Metadata.Count != 0)
			{
				for (int i = 0; i < part.Metadata.Count; i++)
				{
					//part.AddPart(Part.NewValuePart(typeof()));
				}
			}

			foreach (var value in values)
    		{
    			var obj = Activator.CreateInstance(value.Type);
    			BuildProperties(value, obj);
    			list.Add(obj);
    		}
    	}

    	private void BuildProperties(Part part, object result)
        {
            var resultType = result.GetType();
            var properties = part.Parts.Where(n => n is PropertyPart);
            foreach (var property in properties)
            {
                var prop = resultType.GetProperty(property.Name,
                                                  bindingFlags);

            	var propValue = Activator.CreateInstance(property.Type);
                if (prop != null)
                    prop.SetValue(result, propValue, new Object[]{});

				if (propValue is IList)
					BuildValuesForList(property, propValue as IList);
            }
        }
    }
}
