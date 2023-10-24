using GodotAssetLibrary.Common.Domain;
using GodotAssetLibrary.Contracts.Repositories;

namespace GodotAssetLibrary.DataLayer.Repositories
{
    internal class LicenseRepository : ILicenseRepository
    {
        public LicenseRepository(
                    IAssetLibraryContext context)
        {
            Context = context;
        }

        public IAssetLibraryContext Context { get; }

        public async Task<IEnumerable<SoftwareLicense>> GetLicenses(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Context.Licenses);
        }
    }
}
