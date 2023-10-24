using GodotAssetLibrary.Common.Domain;

namespace GodotAssetLibrary.Contracts.Repositories
{
    public interface ILicenseRepository
    {
        Task<IEnumerable<SoftwareLicense>> GetLicenses(CancellationToken cancellationToken = default);
    }
}
