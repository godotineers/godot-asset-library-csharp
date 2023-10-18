using GodotAssetLibrary.Application.Results.Auth;
using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.DataLayer.Services;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class Register : IRequest<RegisterResult>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }


        public class RegisterHandler : IRequestHandler<Register, RegisterResult>
        {
            public RegisterHandler(
                        IUserService userService,
                        IPasswordUtility passwordUtility)
            {
                UserService = userService;
                PasswordUtility = passwordUtility;
            }

            public IUserService UserService { get; }
            public IPasswordUtility PasswordUtility { get; }

            public async Task<RegisterResult> Handle(Register request, CancellationToken cancellationToken)
            {
                var existingUser = await this.UserService.GetUserByUsername(request.Username, cancellationToken);

                if (existingUser != null)
                {
                    return new RegisterResult
                    {
                        Error = "Username already taken."
                    };
                }

                await UserService.Register(request.Username, request.Email, PasswordUtility.Generate(request.Password), cancellationToken);

                return new RegisterResult
                {
                    Username = request.Username,
                    Registered = true,
                    Url = "login",
                };
            }
        }
    }
}
