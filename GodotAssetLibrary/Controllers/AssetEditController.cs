using GodotAssetLibrary.Application.Commands.AssetEdit;
using GodotAssetLibrary.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Controllers
{
    [Route("asset/edit")]
    [Controller]
    public class AssetEditController : Controller
    {
        public IMediator Mediator { get; }

        public AssetEditController(
                    IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [FrontendViewBind(ViewName = "AssetEdits")]
        public async Task<IActionResult> GetAssetEdits(GetAssetEdits getAssetEdits)
        {
            return Ok(await Mediator.Send(getAssetEdits));
        }

        [HttpGet("{id:int}")]
        [FrontendViewBind(ViewName = "AssetEdit")]
        public async Task<IActionResult> GetAssetEdit(int id, GetAssetEdit getAssetEdit)
        {
            return Ok(await Mediator.Send(getAssetEdit));
        }

        [HttpGet("{id:int}/edit")]
        [Authorize]
        [FrontendViewBind(ViewName = "EditAssetEdit")]
        public async Task<IActionResult> EditAsset(int id, EditAsset editAsset)
        {
            return Ok(await Mediator.Send(editAsset));
        }

        [HttpPost("{id:int}")]
        [Authorize]
        public async Task<IActionResult> SubmitAssetEdit(int id, SubmitAssetEdit submitAssetEdit)
        {
            return Ok(await Mediator.Send(submitAssetEdit));
        }

        [HttpPost("{id:int}/accept")]
        public async Task<IActionResult> AcceptAssetEdit(int id, AcceptAssetEdit acceptAssetEdit)
        {
            return Ok(await Mediator.Send(acceptAssetEdit));
        }

        [HttpPost("{id:int}/review")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ReviewAssetEdit(int id, ReviewAssetEdit reviewAssetEdit)
        {
            return Ok(await Mediator.Send(reviewAssetEdit));
        }

        [HttpPost("{id:int}/reject")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> RejectAssetEdit(int id, RejectAssetEdit rejectAssetEdit)
        {
            return Ok(await Mediator.Send(rejectAssetEdit));
        }
    }
}
