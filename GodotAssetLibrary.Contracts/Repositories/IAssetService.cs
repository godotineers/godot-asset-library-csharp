using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public interface IAssetService
    {
        Task ApplyCreationalEdit(Asset asset, CancellationToken cancellationToken = default);
        Task ApplyEdit(Asset asset, CancellationToken cancellationToken = default);
        Task ApplyPreviewEditInsert(AssetPreview preview, CancellationToken cancellationToken = default);
        Task ApplyPreviewEditRemove(int previewId, int assetId, CancellationToken cancellationToken = default);
        Task ApplyPreviewEditUpdate(AssetPreview preview, CancellationToken cancellationToken = default);
        Task CreateAsset(Asset asset, CancellationToken cancellationToken = default);
        Task DeleteAsset(int assetId, CancellationToken cancellationToken = default);
        Task<Asset?> GetAssetBare(int assetId, CancellationToken cancellationToken = default);
        Task<Asset?> GetAssetById(int id, CancellationToken cancellationToken = default);
        Task<AssetPreview?> GetAssetPreviewBare(int previewId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Asset>> SearchAssets(int? category, CategoryTypes? categoryType, SupportLevel[]? supportLevelsRegex, string? username, string? cost, int maxGodotVersion, int minGodotVersion, string filter, string order, string orderDirection, int pageSize, int skipCount, CancellationToken cancellationToken = default);
        Task<int> SearchAssetsCount(int? category, CategoryTypes? categoryType, SupportLevel[]? supportLevelsRegex, string? username, string? cost, int maxGodotVersion, int minGodotVersion, string filter, CancellationToken cancellationToken = default);
        Task SetSupportLevel(int assetId, SupportLevel supportLevel, CancellationToken cancellationToken = default);
        Task UndeleteAsset(int assetId, CancellationToken cancellationToken = default);
    }
}
