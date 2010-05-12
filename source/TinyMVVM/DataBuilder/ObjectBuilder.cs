using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyMVVM.SemanticModel.DataBuilder;

namespace TinyMVVM.DataBuilder
{
    public class ObjectBuilder
    {
        public Object Build(Node node)
        {
            Object result = null;

            result = Activator.CreateInstance(node.Type);

            BuildProperties(node, result);

            if (result is IList)
            {
            	BuildValuesForList(node, result as IList);
            }

        	return result;
        }

    	private void BuildValuesForList(Node node, IList list)
    	{
    		var values = node.Nodes.Where(n => n is ValueNode);
    		foreach (var value in values)
    		{
    			var obj = Activator.CreateInstance(value.Type);
    			BuildProperties(value, obj);
    			list.Add(obj);
    		}
    	}

    	private void BuildProperties(Node node, object result)
        {
            var resultType = result.GetType();
            var properties = node.Nodes.Where(n => n is PropertyNode);
            foreach (var property in properties)
            {
                var prop = resultType.GetProperty(property.Name,
                                                  BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public |
                                                  BindingFlags.NonPublic);

            	var propValue = Activator.CreateInstance(property.Type);
                if (prop != null)
                    prop.SetValue(result, propValue, new Object[]{});

				if (propValue is IList)
					BuildValuesForList(property, propValue as IList);
            }
        }
    }
}
