namespace TinyMVVM.Specifications.DSL
{
	public class All
	{
		public static ISpecification<T> ItemsOf<T>() where T: class
		{
			return new AllSpecification<T>();
		}
	}
}
