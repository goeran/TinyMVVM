using TinyMVVM.Properties;

namespace TinyMVVM.DataBuilder.Internal.Repositories
{
    internal class MaleNameRepository : StringResourceRepository
    {
        protected override string StringResource()
        {
            return Resources.MaleNames;
        }
    }
}
