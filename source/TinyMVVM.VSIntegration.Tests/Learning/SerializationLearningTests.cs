using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NUnit.Framework;
using TinyMVVM.SemanticModel.MVVM;

namespace TinyMVVM.VSIntegration.Tests.Learning
{
	//NB: Don't care about readability

	[TestFixture]
	public class SerializationLearningTests
	{
		private Person person;
		private string serializedObjectAsString;

		[SetUp]
		public void Setup()
		{
			serializedObjectAsString = null;

			person = new Person()
			{
				Name = "Gøran Hansen",
				Age = 28
			};
		}

		[Test]
		public void How_to_serialize_Object_to_string()
		{
			SerializeToString<Person>(person);

			Assert.IsNotNull(serializedObjectAsString);
			Assert.AreNotEqual(string.Empty, serializedObjectAsString);
		}

		private void SerializeToString<T>(Object obj)
		{
			var sb = new StringBuilder();
			using (var stringWriter = new StringWriter(sb))
			{
				var formatter = new XmlSerializer(typeof (T));
				formatter.Serialize(stringWriter, obj);

				serializedObjectAsString = sb.ToString();
			}
		}

		[Test]
		public void How_to_deserialize_Object_from_string()
		{
			SerializeToString<Person>(person);

			Person deserializedObject = DeserializeFromString<Person>();

			Assert.IsNotNull(deserializedObject);
			Assert.AreEqual(person.Age, deserializedObject.Age);
			Assert.AreEqual(person.Name, deserializedObject.Name);
		}

		private T DeserializeFromString<T>()
		{
			var formatter = new XmlSerializer(typeof(T));
			return (T)formatter.Deserialize(new StringReader(serializedObjectAsString));
		}

		[Test]
		[Ignore]
		public void Test_to_serialize_and_deserialize_ModelSpecification()
		{
			var modelSpec = new ModelSpecification();

			SerializeToString<ModelSpecification>(modelSpec);

			Assert.IsNotNull(DeserializeFromString<ModelSpecification>());


		}

		[Serializable]
		public class Person
		{
			public string Name { get; set; }
			public int Age { get; set; }
			public System.Collections.Generic.List<string> Test { get; set; }
		}
	}
}
