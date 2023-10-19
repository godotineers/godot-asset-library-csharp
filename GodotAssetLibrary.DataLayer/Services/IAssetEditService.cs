using GodotAssetLibrary.Common;
using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public interface IAssetEditService
    {
        Task AddAssetEditPreview(AssetEditPreview assetEditPreview, CancellationToken cancellationToken = default);
        int CountAssetEdits(string statusesRegex, int assetId, string username, string filter);
        AssetEdit? GetAssetEditBare(int editId);
        AssetEdit? GetAssetEditById(int editId);
        AssetEdit? GetAssetEditWithStatus(int editId, EditStatus status);
        IQueryable<AssetEdit> GetEditableAssetEditsByAssetId(int assetId);
        Task<IEnumerable<EditEvent>> ListEditEvents(int userId, int pageSize, int skipCount, CancellationToken cancellationToken = default);
        Task RemoveAssetEditPreview(int editPreviewId, CancellationToken cancellationToken = default);
        IQueryable<AssetEdit> SearchAssetEdits(string statusesRegex, int assetId, string username, string filter, int pageSize, int skipCount);
        Task SetAssetEditAssetId(int editId, int assetId, CancellationToken cancellationToken = default);
        Task SetAssetEditStatusAndReason(int editId, EditStatus status, string reason, CancellationToken cancellationToken = default);
        Task SubmitAssetEdit(AssetEdit assetEdit, CancellationToken cancellationToken = default);
        Task UpdateAssetEdit(AssetEdit assetEdit, CancellationToken cancellationToken = default);
        Task UpdateAssetEditPreview(AssetEditPreview updatedAssetEditPreview, CancellationToken cancellationToken = default);
    }
}
