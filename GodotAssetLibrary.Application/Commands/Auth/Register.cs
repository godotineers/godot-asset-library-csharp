using FluentValidation;
using FluentValidation.Results;
using GodotAssetLibrary.Application.Results.Auth;
using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.DataLayer.Services;
using MediatR;
using Microsoft.Win32;
using System;

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

        public class Validator : AbstractValidator<Register>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
                RuleFor(x => x.Email).NotNull().NotEmpty();
            }
        }
    }
}
