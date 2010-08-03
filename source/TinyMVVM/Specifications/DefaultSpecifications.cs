namespace TinyMVVM.Specifications
{
	public class AllSpecification<T> : ISpecification<T> where T: class
	{
		public bool IsSatisfiedBy(T val)
		{
			return true;
		}
	}
}
