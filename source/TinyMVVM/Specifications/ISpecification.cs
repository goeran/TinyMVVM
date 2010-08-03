namespace TinyMVVM.Specifications
{
	public interface ISpecification<T> where T: class
	{
		bool IsSatisfiedBy(T val);
	}
}
