using GodotAssetLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.DataLayer
{
    public class AssetLibraryContext : DbContext, IAssetLibraryContext
    {
        public AssetLibraryContext(DbContextOptions<AssetLibraryContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetEdit> AssetEdits { get; set; }
        public DbSet<AssetEditPreview> AssetEditPreviews { get; set; }
        public DbSet<AssetPreview> AssetPreviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Keys are already defined using the [Key] attribute in the model classes.

            // Indexes
            modelBuilder.Entity<Asset>().HasIndex(a => a.GodotVersion, "godot_version_index");
            modelBuilder.Entity<AssetEdit>().HasIndex(ae => ae.GodotVersion, "godot_version_index");
            modelBuilder.Entity<AssetEdit>().HasIndex(ae => ae.AssetId);
            modelBuilder.Entity<AssetEdit>().HasIndex(ae => ae.Status);
            modelBuilder.Entity<AssetEditPreview>().HasIndex(aep => aep.EditId);
            modelBuilder.Entity<AssetEditPreview>().HasIndex(aep => aep.Type);
            modelBuilder.Entity<AssetEditPreview>().HasIndex(aep => aep.PreviewId);
            modelBuilder.Entity<AssetPreview>().HasIndex(ap => ap.AssetId);
            modelBuilder.Entity<AssetPreview>().HasIndex(ap => ap.Type);
            modelBuilder.Entity<User>().HasIndex(u => u.ResetToken).IsUnique();

            // Unique Keys
            modelBuilder.Entity<Category>().HasIndex(c => c.CategoryName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.SessionToken).IsUnique();
        }
    }
}
