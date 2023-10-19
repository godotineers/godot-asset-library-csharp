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
        public async Task<IActionResult> Logout(Logout logout)
        {
            return Ok(await Mediator.Send(logout));
        }

        [HttpPost("forgot_password")]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            return Ok(await Mediator.Send(forgotPassword));
        }

        [HttpGet("reset_password")]
        public async Task<IActionResult> GetResetPassword(GetResetPassword getResetPassword)
        {
            return Ok(await Mediator.Send(getResetPassword));
        }

        [HttpPost("reset_password")]
        [Authorize]
        public async Task<IActionResult> PostResetPassword(PostResetPassword postResetPassword)
        {
            return Ok(await Mediator.Send(postResetPassword));
        }

        [HttpPost("change_password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            return Ok(await Mediator.Send(changePassword));
        }
    }
}
