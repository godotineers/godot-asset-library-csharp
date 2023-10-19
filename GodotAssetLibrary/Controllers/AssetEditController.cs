using GodotAssetLibrary.Application.Commands.AssetEdit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Controllers
{
    [Route("asset")]
    [Controller]
    public class AssetEditController : ControllerBase
    {
        public IMediator Mediator { get; }

        public AssetEditController(
                    IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet("edit")]
        public async Task<IActionResult> GetAssetEdits(GetAssetEdits getAssetEdits)
        {
            return Ok(await Mediator.Send(getAssetEdits));
        }

        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> GetAssetEdit(int id, GetAssetEdit getAssetEdit)
        {
            return Ok(await Mediator.Send(getAssetEdit));
        }

        [HttpGet("edit/{id:int}/edit")]
        [Authorize]
        public async Task<IActionResult> EditAsset(int id, EditAsset editAsset)
        {
            return Ok(await Mediator.Send(editAsset));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsset(CreateAsset createAsset)
        {
            return Ok(await Mediator.Send(createAsset));
        }

        [HttpPost("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsset(int id, UpdateAsset updateAsset)
        {
            return Ok(await Mediator.Send(updateAsset));
        }

        [HttpPost("edit/{id:int}")]
        [Authorize]
        public async Task<IActionResult> SubmitAssetEdit(int id, SubmitAssetEdit submitAssetEdit)
        {
            return Ok(await Mediator.Send(submitAssetEdit));
        }

        [HttpPost("edit/{id:int}/accept")]
        public async Task<IActionResult> AcceptAssetEdit(int id, AcceptAssetEdit acceptAssetEdit)
        {
            return Ok(await Mediator.Send(acceptAssetEdit));
        }

        [HttpPost("edit/{id:int}/review")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ReviewAssetEdit(int id, ReviewAssetEdit reviewAssetEdit)
        {
            return Ok(await Mediator.Send(reviewAssetEdit));
        }

        [HttpPost("edit/{id:int}/reject")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> RejectAssetEdit(int id, RejectAssetEdit rejectAssetEdit)
        {
            return Ok(await Mediator.Send(rejectAssetEdit));
        }
    }
}
