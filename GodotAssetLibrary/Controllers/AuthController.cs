using CommunityToolkit.Diagnostics;
using GodotAssetLibrary.Application.Commands.Auth;
using GodotAssetLibrary.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
// ... other necessary using directives ...

namespace GodotAssetLibrary.Controllers
{
    [Route("auth")]
    [Controller]
    public class AuthController : Controller
    {
        public IMediator Mediator { get; }

        public AuthController(
                IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet("configure")]
        public IActionResult Configure()
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public IActionResult Register(/* parameters here */)
        {
            throw new NotImplementedException();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthLoginRequest loginRequest)
        {
            Guard.IsNotNullOrWhiteSpace(loginRequest.Username, nameof(loginRequest.Username));
            Guard.IsNotNullOrWhiteSpace(loginRequest.Password, nameof(loginRequest.Password));

            return Ok(await Mediator.Send(new Login { Username = loginRequest.Username, Password = loginRequest.Password, Token = loginRequest.Token }));
        }

        [HttpGet("logout")]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }

        [HttpPost("forgot_password")]
        public IActionResult ForgotPassword(/* parameters here */)
        {
            throw new NotImplementedException();
        }

        [HttpGet("reset_password")]
        public IActionResult GetResetPassword()
        {
            throw new NotImplementedException();
        }

        [HttpPost("reset_password")]
        public IActionResult PostResetPassword(/* parameters here */)
        {
            throw new NotImplementedException();
        }

        [HttpPost("change_password")]
        public IActionResult ChangePassword(/* parameters here */)
        {
            throw new NotImplementedException();
        }
    }
}
