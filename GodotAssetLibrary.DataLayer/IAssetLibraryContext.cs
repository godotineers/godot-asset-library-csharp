using GodotAssetLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.DataLayer
{
    public interface IAssetLibraryContext
    {
        DbSet<AssetEditPreview> AssetEditPreviews { get; set; }
        DbSet<AssetEdit> AssetEdits { get; set; }
        DbSet<AssetPreview> AssetPreviews { get; set; }
        DbSet<Asset> Assets { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
