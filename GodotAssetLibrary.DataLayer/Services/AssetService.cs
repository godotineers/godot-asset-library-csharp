using GodotAssetLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.DataLayer.Services
{
    public class AssetService
    {
        private readonly AssetLibraryContext _context;

        public AssetService(AssetLibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Asset> SearchAssets(
            int category,
            CategoryTypes categoryType,
            string supportLevelsRegex,
            string username,
            string cost,
            int maxGodotVersion,
            int minGodotVersion,
            string filter,
            string order,
            string orderDirection,
            int pageSize,
            int skipCount)
        {
            var query = _context.Assets
                .Include(a => a.User)
                .Include(a => a.Category)
                .Where(a => a.Searchable == true
                    && a.CategoryId == category
                    && a.Category.CategoryType == categoryType
                    && EF.Functions.Like(a.SupportLevel.ToString(), supportLevelsRegex)
                    && EF.Functions.Like(a.User.Username, username)
                    && EF.Functions.Like(a.Cost, cost)
                    && a.GodotVersion <= maxGodotVersion
                    && a.GodotVersion >= minGodotVersion
                    && (EF.Functions.Like(a.Title, filter)
                        || EF.Functions.Like(a.Cost, filter)
                        || EF.Functions.Like(a.User.Username, filter)));

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
            return query.Skip(skipCount).Take(pageSize).ToList();
        }

        public int SearchAssetsCount(
            int category,
            CategoryTypes categoryType,
            string supportLevelsRegex,
            string username,
            string cost,
            int maxGodotVersion,
            int minGodotVersion,
            string filter)
        {
            return _context.Assets
                .Include(a => a.User)
                .Include(a => a.Category)
                .Where(a => a.Searchable == true
                    && a.Category.CategoryType == categoryType
                    && a.CategoryId == category
                    && EF.Functions.Like(a.SupportLevel.ToString(), supportLevelsRegex)
                    && EF.Functions.Like(a.User.Username, username)
                    && EF.Functions.Like(a.Cost, cost)
                    && a.GodotVersion <= maxGodotVersion
                    && a.GodotVersion >= minGodotVersion
                    && (EF.Functions.Like(a.Title, filter)
                        || EF.Functions.Like(a.Cost, filter)
                        || EF.Functions.Like(a.User.Username, filter)))
                .Count();
        }

        public void CreateAsset(Asset asset)
        {
            _context.Assets.Add(asset);
            _context.SaveChanges();
        }

        public Asset GetAssetById(int id)
        {
            return _context.Assets
                .Include(a => a.Category)
                .Include(a => a.User)
                .Include(a => a.Preview)
                .SingleOrDefault(a => a.AssetId == id);
        }

        public Asset GetAssetBare(int assetId)
        {
            return _context.Assets.Find(assetId);
        }

        public AssetPreview GetAssetPreviewBare(int previewId)
        {
            return _context.AssetPreviews.Find(previewId);
        }

        public void ApplyCreationalEdit(Asset asset)
        {
            _context.Assets.Add(asset);
            _context.SaveChanges();
        }

        public void ApplyEdit(Asset asset)
        {
            _context.Assets.Update(asset);
            _context.SaveChanges();
        }

        public void ApplyPreviewEditInsert(AssetPreview preview)
        {
            _context.AssetPreviews.Add(preview);
            _context.SaveChanges();
        }

        public void ApplyPreviewEditRemove(int previewId, int assetId)
        {
            var preview = _context.AssetPreviews.SingleOrDefault(p => p.PreviewId == previewId && p.AssetId == assetId);
            if (preview != null)
            {
                _context.AssetPreviews.Remove(preview);
                _context.SaveChanges();
            }
        }

        public void ApplyPreviewEditUpdate(AssetPreview preview)
        {
            _context.AssetPreviews.Update(preview);
            _context.SaveChanges();
        }

        public void SetSupportLevel(int assetId, int supportLevel)
        {
            var asset = _context.Assets.Find(assetId);
            if (asset != null)
            {
                asset.SupportLevel = supportLevel;
                _context.SaveChanges();
            }
        }

        public void DeleteAsset(int assetId)
        {
            var asset = _context.Assets.Find(assetId);
            if (asset != null)
            {
                asset.Searchable = false;
                _context.SaveChanges();
            }
        }

        public void UndeleteAsset(int assetId)
        {
            var asset = _context.Assets.Find(assetId);
            if (asset != null)
            {
                asset.Searchable = true;
                _context.SaveChanges();
            }
        }
    }
}
