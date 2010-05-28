using TinyMVVM.Properties;

namespace TinyMVVM.DataBuilder.Internal.Repositories
{
    internal class FemaleNameRepository : StringResourceRepository
    {
        protected override string StringResource()
        {
            return Resources.FemaleNames;
        }
    }
}
