using GodotAssetLibrary.Application.Commands.User;
using Microsoft.AspNetCore.Mvc;

namespace GodotAssetLibrary.Contracts
{
    public interface IUserController
    {
        Task<IActionResult> GetUserFeed(UserFeed userFeed);
    }
}
