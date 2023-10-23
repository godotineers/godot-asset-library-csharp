using GodotAssetLibrary.Application.Commands.User;
using GodotAssetLibrary.Attributes;
using GodotAssetLibrary.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Controllers
{
    [Route("user")]
    [Controller]
    [Authorize]
    public class UserController : Controller, IUserController
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
