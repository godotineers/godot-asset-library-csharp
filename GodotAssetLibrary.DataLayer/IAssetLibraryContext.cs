using GodotAssetLibrary.Common.Domain;
using GodotAssetLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.DataLayer
{
    public interface IAssetLibraryContext
    {
        DbSet<AssetEditPreview> AssetEditPreviews { get; }
        DbSet<AssetEdit> AssetEdits { get; }
        DbSet<AssetPreview> AssetPreviews { get; }
        DbSet<Asset> Assets { get; }
        DbSet<Category> Categories { get; }
        DbSet<User> Users { get; }

        DbSet<SoftwareLicense> Licenses { get; }

        DbSet<GodotVersion> Versions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
