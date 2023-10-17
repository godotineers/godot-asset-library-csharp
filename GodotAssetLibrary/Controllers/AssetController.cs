using GodotAssetLibrary.Application.Commands.Assets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Controllers
{
    [Route("asset")]
    [Controller]
    public class AssetController : Controller
    {
        public IMediator Mediator { get; }

        public AssetController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            return Ok(await Mediator.Send(new GetAssets { }));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            return View("Asset", await Mediator.Send(new GetAssetById { AssetId = id }));
        }

        [HttpPost("{id:int}/support_level")]
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
