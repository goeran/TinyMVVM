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

		public Node(Type type)
		{
			if (type == null) throw new ArgumentNullException();

			Type = type;
		}

		public Node NewNode()
		{
			var leaf = new Node(typeof(string));
			AddNode(leaf);

			return leaf;
		}

		private void AddNode(Node node)
		{
			if (node == null) throw new ArgumentNullException();

			nodes.Add(node);
		}
	}
}
