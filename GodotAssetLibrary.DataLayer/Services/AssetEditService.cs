using System.Linq;
using Microsoft.EntityFrameworkCore;
using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public class AssetEditService
    {
        private readonly AssetLibraryContext _context;

        public AssetEditService(AssetLibraryContext context)
        {
            _context = context;
        }

        public AssetEdit GetAssetEditById(int editId)
        {
            return _context.AssetEdits
                .Include(a => a.User)
                .Include(a => a.AssetEditPreviews)
                .ThenInclude(p => p.Preview)
                .SingleOrDefault(a => a.EditId == editId);
        }

        public AssetEdit GetAssetEditBare(int editId)
        {
            return _context.AssetEdits.Find(editId);
        }

        public AssetEdit GetAssetEditWithStatus(int editId, int status)
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


        public void SubmitAssetEdit(AssetEdit assetEdit)
        {
            _context.AssetEdits.Add(assetEdit);
            _context.SaveChanges();
        }

        public void UpdateAssetEdit(AssetEdit assetEdit)
        {
            _context.AssetEdits.Update(assetEdit);
            _context.SaveChanges();
        }

        public void AddAssetEditPreview(AssetEditPreview assetEditPreview)
        {
            _context.AssetEditPreviews.Add(assetEditPreview);
            _context.SaveChanges();
        }

        public void UpdateAssetEditPreview(AssetEditPreview updatedAssetEditPreview)
        {
            var existingPreview = _context.AssetEditPreviews.SingleOrDefault(a => a.EditId == updatedAssetEditPreview.EditId && a.EditPreviewId == updatedAssetEditPreview.EditPreviewId);
            if (existingPreview != null)
            {
                existingPreview.Type = updatedAssetEditPreview.Type ?? existingPreview.Type;
                existingPreview.Link = updatedAssetEditPreview.Link ?? existingPreview.Link;
                existingPreview.Thumbnail = updatedAssetEditPreview.Thumbnail ?? existingPreview.Thumbnail;
                _context.SaveChanges();
            }
        }

        public void RemoveAssetEditPreview(int editPreviewId)
        {
            var editPreview = _context.AssetEditPreviews.Find(editPreviewId);
            if (editPreview != null)
            {
                _context.AssetEditPreviews.Remove(editPreview);
                _context.SaveChanges();
            }
        }

        public void SetAssetEditAssetId(int editId, int assetId)
        {
            var assetEdit = _context.AssetEdits.Find(editId);
            if (assetEdit != null)
            {
                assetEdit.AssetId = assetId;
                _context.SaveChanges();
            }
        }

        public void SetAssetEditStatusAndReason(int editId, int status, string reason)
        {
            var assetEdit = _context.AssetEdits.Find(editId);
            if (assetEdit != null)
            {
                assetEdit.Status = status;
                assetEdit.Reason = reason;
                _context.SaveChanges();
            }
        }
    }
}
