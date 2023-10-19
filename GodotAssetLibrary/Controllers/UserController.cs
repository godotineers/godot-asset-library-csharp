using GodotAssetLibrary.Application.Commands.User;
using GodotAssetLibrary.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Controllers
{
    [Route("user")]
    [Controller]
    [Authorize]
    public class UserController : Controller
    {
        public UserController(
                    IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        [HttpGet("feed")]
        [Authorize]
        [FrontendViewBind(ViewName = "Feed")]
        public async Task<IActionResult> GetUserFeed(UserFeed userFeed)
        {
            return Ok(await Mediator.Send(userFeed));
        }

    }
}
