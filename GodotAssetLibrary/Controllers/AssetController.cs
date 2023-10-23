using GodotAssetLibrary.Application.Commands.Assets;
using GodotAssetLibrary.Application.ViewModels;
using GodotAssetLibrary.Attributes;
using GodotAssetLibrary.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Controllers
{
    [Route("asset")]
    [Controller]
    public class AssetController : Controller, IAssetController
    {
        public IMediator Mediator { get; }

        public AssetController(
                    IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [FrontendViewBind(ViewName = "Assets")]
        public async Task<IActionResult> GetAssets(GetAssets getAssets)
        {
            return Ok(await Mediator.Send(getAssets));
        }

        [HttpGet("{id:int}")]
        [FrontendViewBind(ViewName = "Asset")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            return View("Asset", await Mediator.Send(new GetAssetById { AssetId = id }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsset(CreateAsset createAsset)
        {
            return Ok(await Mediator.Send(createAsset));
        }

        [HttpGet("submit")]
        [BindSelectItemsToViewBag]
        public async Task<IActionResult> SubmitAssetForm()
        {
            return View("SubmitAsset", new AssetViewModel()
            {

            });
        }

        [HttpPost("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsset(int id, UpdateAsset updateAsset)
        {
            return Ok(await Mediator.Send(updateAsset));
        }

        [HttpPost("{id:int}/support_level")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> UpdateSupportLevel(int id)
        {
            return Ok(await Mediator.Send(new UpdateSupportLevel { AssetId = id }));
        }

        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            return Ok(await Mediator.Send(new DeleteAsset { AssetId = id }));
        }

        [HttpPost("{id:int}/undelete")]
        public async Task<IActionResult> UndeleteAsset(int id)
        {
            return Ok(await Mediator.Send(new UndeleteAsset { AssetId = id }));
        }
    }
}
