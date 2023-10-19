using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.DataLayer.Services
{
    internal class AssetService : IAssetService
    {
        private readonly IAssetLibraryContext _context;

        public AssetService(IAssetLibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> SearchAssets(
            int? category,
            CategoryTypes? categoryType,
            SupportLevel[]? supportLevels,
            string? username,
            string? cost,
            int maxGodotVersion,
            int minGodotVersion,
            string filter,
            string order,
            string orderDirection,
            int pageSize,
            int skipCount,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Assets
                .Include(a => a.User)
                .Include(a => a.Category)
                .Where(a => a.Searchable == true
                    && a.GodotVersion <= maxGodotVersion
                    && a.GodotVersion >= minGodotVersion
                    && (EF.Functions.Like(a.Title, filter)
                        || EF.Functions.Like(a.Cost, filter)
                        || EF.Functions.Like(a.User.Username, filter)));


            if (category.HasValue)
            {
                query = query.Where(a => a.CategoryId == category);
            }

            if (categoryType.HasValue)
            {
                query = query.Where(a => a.Category.CategoryType == categoryType);
            }

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(a => a.User.Username == username);
            }

            if (!string.IsNullOrEmpty(cost))
            {
                query = query.Where(a => a.Cost == cost);
            }

            if (supportLevels != null && supportLevels.Any())
            {
                query = query.Where(a => supportLevels.Any(x => x == a.SupportLevel));
            }


            // OrderBy logic based on order and orderDirection parameters
            if (orderDirection == "asc")
            {
                switch (order)
                {
                    case "rating":
                        query = query.OrderBy(a => a.Rating);
                        break;
                    case "cost":
                        query = query.OrderBy(a => a.Cost);
                        break;
                    case "title":
                        query = query.OrderBy(a => a.Title);
                        break;
                    case "modify_date":
                        query = query.OrderBy(a => a.ModifyDate);
                        break;
                }
            }
            else if (orderDirection == "desc")
            {
                switch (order)
                {
                    case "rating":
                        query = query.OrderByDescending(a => a.Rating);
                        break;
                    case "cost":
                        query = query.OrderByDescending(a => a.Cost);
                        break;
                    case "title":
                        query = query.OrderByDescending(a => a.Title);
                        break;
                    case "modify_date":
                        query = query.OrderByDescending(a => a.ModifyDate);
                        break;
                }
            }

            // Apply pagination
            return await query.Skip(skipCount).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<int> SearchAssetsCount(
            int? category,
            CategoryTypes? categoryType,
            SupportLevel[]? supportLevels,
            string? username,
            string? cost,
            int maxGodotVersion,
            int minGodotVersion,
            string filter,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Assets
                .Include(a => a.User)
                .Include(a => a.Category)
                .Where(a => a.Searchable == true
                    && a.GodotVersion <= maxGodotVersion
                    && a.GodotVersion >= minGodotVersion
                    && (EF.Functions.Like(a.Title, filter)
                        || EF.Functions.Like(a.Cost, filter)
                        || EF.Functions.Like(a.User.Username, filter)));

            if (category.HasValue)
            {
                query = query.Where(a => a.CategoryId == category);
            }

            if (categoryType.HasValue)
            {
                query = query.Where(a => a.Category.CategoryType == categoryType);
            }

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(a => a.User.Username == username);
            }

            if (!string.IsNullOrEmpty(cost))
            {
                query = query.Where(a => a.Cost == cost);
            }

            if (supportLevels != null && supportLevels.Any())
            {
                query = query.Where(a => supportLevels.Any(x => x == a.SupportLevel));
            }

            return await query.CountAsync(cancellationToken);
        }

        public async Task CreateAsset(Asset asset, CancellationToken cancellationToken = default)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Asset?> GetAssetById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.User)
                .Include(a => a.Preview)
                .SingleOrDefaultAsync(a => a.AssetId == id);
        }

        public async Task<Asset?> GetAssetBare(int assetId, CancellationToken cancellationToken = default)
        {
            return await _context.Assets.FindAsync(assetId);
        }

        public async Task<AssetPreview?> GetAssetPreviewBare(int previewId, CancellationToken cancellationToken = default)
        {
            return await _context.AssetPreviews.FindAsync(previewId);
        }

        public async Task ApplyCreationalEdit(Asset asset, CancellationToken cancellationToken = default)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ApplyEdit(Asset asset, CancellationToken cancellationToken = default)
        {
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ApplyPreviewEditInsert(AssetPreview preview, CancellationToken cancellationToken = default)
        {
            _context.AssetPreviews.Add(preview);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ApplyPreviewEditRemove(int previewId, int assetId, CancellationToken cancellationToken = default)
        {
            var preview = _context.AssetPreviews.SingleOrDefault(p => p.PreviewId == previewId && p.AssetId == assetId);
            if (preview != null)
            {
                _context.AssetPreviews.Remove(preview);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task ApplyPreviewEditUpdate(AssetPreview preview, CancellationToken cancellationToken = default)
        {
            _context.AssetPreviews.Update(preview);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SetSupportLevel(int assetId, SupportLevel supportLevel, CancellationToken cancellationToken = default)
        {
            var asset = _context.Assets.Find(assetId);
            if (asset != null)
            {
                asset.SupportLevel = supportLevel;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsset(int assetId, CancellationToken cancellationToken = default)
        {
            var asset = _context.Assets.Find(assetId);
            if (asset != null)
            {
                asset.Searchable = false;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UndeleteAsset(int assetId, CancellationToken cancellationToken = default)
        {
            var asset = _context.Assets.Find(assetId);
            if (asset != null)
            {
                asset.Searchable = true;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
