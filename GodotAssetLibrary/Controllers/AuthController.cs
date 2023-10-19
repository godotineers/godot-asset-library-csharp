using CommunityToolkit.Diagnostics;
using GodotAssetLibrary.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Configure(Configure configure)
        {
            return Ok(await Mediator.Send(configure));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            Guard.IsNotNullOrWhiteSpace(register.Username, nameof(register.Username));
            Guard.IsNotNullOrWhiteSpace(register.Password, nameof(register.Password));
            Guard.IsNotNullOrWhiteSpace(register.Email, nameof(register.Email));

            return Ok(await Mediator.Send(register));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login loginRequest)
        {
            Guard.IsNotNullOrWhiteSpace(loginRequest.Username, nameof(loginRequest.Username));
            Guard.IsNotNullOrWhiteSpace(loginRequest.Password, nameof(loginRequest.Password));

            return Ok(await Mediator.Send(loginRequest));
        }

        [HttpGet("logout")]
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout(Logout logout)
        {
            throw new NotImplementedException();
        }

        [HttpPost("forgot_password")]
        public IActionResult ForgotPassword()
        {
            throw new NotImplementedException();
        }

        [HttpGet("reset_password")]
        public IActionResult GetResetPassword()
        {
            throw new NotImplementedException();
        }

        [HttpPost("reset_password")]
        [Authorize]
        public IActionResult PostResetPassword(/* parameters here */)
        {
            throw new NotImplementedException();
        }

        [HttpPost("change_password")]
        [Authorize]
        public IActionResult ChangePassword(/* parameters here */)
        {
            throw new NotImplementedException();
        }
    }
}
