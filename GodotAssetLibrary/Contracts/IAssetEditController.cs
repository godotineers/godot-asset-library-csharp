using GodotAssetLibrary.Application.Commands.AssetEdit;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Contracts
{
    public interface IAssetEditController
    {
        Task<IActionResult> AcceptAssetEdit(int id, AcceptAssetEdit acceptAssetEdit);
        Task<IActionResult> EditAsset(int id, EditAsset editAsset);
        Task<IActionResult> GetAssetEdit(int id, GetAssetEdit getAssetEdit);
        Task<IActionResult> GetAssetEdits(GetAssetEdits getAssetEdits);
        Task<IActionResult> RejectAssetEdit(int id, RejectAssetEdit rejectAssetEdit);
        Task<IActionResult> ReviewAssetEdit(int id, ReviewAssetEdit reviewAssetEdit);
        Task<IActionResult> SubmitAssetEdit(int id, SubmitAssetEdit submitAssetEdit);
    }
}
