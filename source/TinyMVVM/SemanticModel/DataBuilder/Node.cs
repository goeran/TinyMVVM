using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DataBuilder
{
	public class Node
	{
		private List<Node> nodes = new List<Node>();

		public ReadOnlyCollection<Node> Nodes
		{
			get
			{
				return new ReadOnlyCollection<Node>(nodes);
			}
		}

		public Type Type { get; private set; }
		public Node Parent { get; set; }
	    public string Name { get; set; }

		public bool IsRoot
		{
			get { return Parent == null; }
		}

		public Node(Type type)
		{
			if (type == null) throw new ArgumentNullException();

			Type = type;
		}

		public static Node NewPropertyNode(string name, Type type)
		{
            if (type == null) throw new ArgumentNullException();
            if (name == null) throw new ArgumentNullException();

			var propertyNode = new PropertyNode(type);
		    propertyNode.Name = name;

			return propertyNode;
		}

		public void AddNode(Node node)
		{
			if (node == null) throw new ArgumentNullException();

            node.Parent = this;
			nodes.Add(node);
		}

	    public static Node NewValueNode(Type type)
	    {
            if (type == null) throw new ArgumentNullException();

	        var valueNode = new ValueNode(type);

	        return valueNode;
	    }
	}
}
