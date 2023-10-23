using GodotAssetLibrary.Application.Commands.Assets;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Contracts
{
    public interface IAssetController
    {
        Task<IActionResult> CreateAsset(CreateAsset createAsset);
        Task<IActionResult> DeleteAsset(int id);
        Task<IActionResult> GetAssetById(int id);
        Task<IActionResult> GetAssets(GetAssets getAssets);
        Task<IActionResult> SubmitAssetForm();
        Task<IActionResult> UndeleteAsset(int id);
        Task<IActionResult> UpdateAsset(int id, UpdateAsset updateAsset);
        Task<IActionResult> UpdateSupportLevel(int id);
    }
}