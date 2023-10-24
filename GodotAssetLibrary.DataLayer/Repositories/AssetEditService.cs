using GodotAssetLibrary.Common;
using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.DataLayer.Services
{
    internal class AssetEditService : IAssetEditService
    {
        private readonly IAssetLibraryContext _context;

        public AssetEditService(IAssetLibraryContext context)
        {
            _context = context;
        }

        public AssetEdit? GetAssetEditById(int editId)
        {
            return _context.AssetEdits
                .Include(a => a.User)
                .Include(a => a.AssetEditPreviews)
                .ThenInclude(p => p.Preview)
                .SingleOrDefault(a => a.EditId == editId);
        }

        public AssetEdit? GetAssetEditBare(int editId)
        {
            return _context.AssetEdits.Find(editId);
        }

        public AssetEdit? GetAssetEditWithStatus(int editId, EditStatus status)
        {
            return _context.AssetEdits.SingleOrDefault(a => a.EditId == editId && a.Status == status);
        }

        public IQueryable<AssetEdit> GetEditableAssetEditsByAssetId(int assetId)
        {
            return _context.AssetEdits
                .Where(a => a.AssetId == assetId && a.Status == 0);
        }

        public IQueryable<AssetEdit> SearchAssetEdits(
                    string statusesRegex,
                    int assetId,
                    string username,
                    string filter,
                    int pageSize,
                    int skipCount)
        {
            return _context.AssetEdits
                .Include(a => a.User)
                .Include(a => a.Category)
                .Include(a => a.Asset)
                .Where(a => EF.Functions.Like(a.Status.ToString(), statusesRegex)
                    && EF.Functions.Like(a.AssetId.ToString(), assetId.ToString())
                    && EF.Functions.Like(a.User.Username, username)
                    && (EF.Functions.Like(a.Title, filter)
                        || EF.Functions.Like(a.Asset.Title, filter)
                        || EF.Functions.Like(a.User.Username, filter)))
                .OrderByDescending(a => a.ModifyDate)
                .Skip(skipCount)
                .Take(pageSize);
        }

        public int CountAssetEdits(
                    string statusesRegex,
                    int assetId,
                    string username,
                    string filter)
        {
            return _context.AssetEdits
                .Include(a => a.User)
                .Where(a => EF.Functions.Like(a.Status.ToString(), statusesRegex)
                    && EF.Functions.Like(a.AssetId.ToString(), assetId.ToString())
                    && EF.Functions.Like(a.User.Username, username)
                    && (EF.Functions.Like(a.Title, filter)
                        || EF.Functions.Like(a.User.Username, filter)))
                .Count();
        }


        public async Task SubmitAssetEdit(AssetEdit assetEdit, CancellationToken cancellationToken = default)
        {
            _context.AssetEdits.Add(assetEdit);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAssetEdit(AssetEdit assetEdit, CancellationToken cancellationToken = default)
        {
            _context.AssetEdits.Update(assetEdit);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAssetEditPreview(AssetEditPreview assetEditPreview, CancellationToken cancellationToken = default)
        {
            _context.AssetEditPreviews.Add(assetEditPreview);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAssetEditPreview(AssetEditPreview updatedAssetEditPreview, CancellationToken cancellationToken = default)
        {
            var existingPreview = _context.AssetEditPreviews.SingleOrDefault(a => a.EditId == updatedAssetEditPreview.EditId && a.EditPreviewId == updatedAssetEditPreview.EditPreviewId);
            if (existingPreview != null)
            {
                existingPreview.Type = updatedAssetEditPreview.Type ?? existingPreview.Type;
                existingPreview.Link = updatedAssetEditPreview.Link ?? existingPreview.Link;
                existingPreview.Thumbnail = updatedAssetEditPreview.Thumbnail ?? existingPreview.Thumbnail;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task RemoveAssetEditPreview(int editPreviewId, CancellationToken cancellationToken = default)
        {
            var editPreview = _context.AssetEditPreviews.Find(editPreviewId);
            if (editPreview != null)
            {
                _context.AssetEditPreviews.Remove(editPreview);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SetAssetEditAssetId(int editId, int assetId, CancellationToken cancellationToken = default)
        {
            var assetEdit = _context.AssetEdits.Find(editId);
            if (assetEdit != null)
            {
                assetEdit.AssetId = assetId;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SetAssetEditStatusAndReason(int editId, EditStatus status, string reason, CancellationToken cancellationToken = default)
        {
            var assetEdit = _context.AssetEdits.Find(editId);
            if (assetEdit != null)
            {
                assetEdit.Status = status;
                assetEdit.Reason = reason;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }


        public async Task<IEnumerable<EditEvent>> ListEditEvents(int userId, int pageSize, int skipCount, CancellationToken cancellationToken = default)
        {
            var query = _context.AssetEdits
                .Where(edit => edit.UserId == userId)  // Filtering by user_id
                .OrderByDescending(edit => edit.ModifyDate)  // Ordering by modify_date
                .Select(edit => new EditEvent // Projecting to a new result form, similar to the SELECT statement in SQL
                {
                    EditId = edit.EditId,
                    AssetId = edit.AssetId,
                    Title = edit.Title ?? edit.Asset.Title,  // COALESCE equivalent
                    SubmitDate = edit.SubmitDate,
                    ModifyDate = edit.ModifyDate,
                    Category = edit.Category.CategoryName,  // Assuming you have navigation properties set up correctly
                    VersionString = edit.VersionString ?? edit.Asset.VersionString,  // COALESCE equivalent
                    IconUrl = edit.IconUrl ?? edit.Asset.IconUrl,  // COALESCE equivalent
                    Status = edit.Status.ToString(),
                    Reason = edit.Reason
                });

            // Apply paging
            return await query
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
