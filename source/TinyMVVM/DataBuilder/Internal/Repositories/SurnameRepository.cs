using TinyMVVM.Properties;

namespace TinyMVVM.DataBuilder.Internal.Repositories
{
    internal class SurnameRepository : StringResourceRepository
    {
        protected override string StringResource()
        {
            return Resources.Surnames;
        }
    }
}
