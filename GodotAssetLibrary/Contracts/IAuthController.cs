using GodotAssetLibrary.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Contracts
{
    public interface IAuthController
    {
        IMediator Mediator { get; }

        Task<IActionResult> ChangePassword(ChangePassword changePassword);
        Task<IActionResult> Configure(Configure configure);
        Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword);
        Task<IActionResult> GetResetPassword(GetResetPassword getResetPassword);
        Task<IActionResult> Login(Login loginRequest);
        Task<IActionResult> Logout(Logout logout);
        Task<IActionResult> PostResetPassword(PostResetPassword postResetPassword);
        Task<IActionResult> Register(Register register);
    }
}