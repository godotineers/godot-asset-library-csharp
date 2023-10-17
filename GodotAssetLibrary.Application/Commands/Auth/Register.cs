using GodotAssetLibrary.Application.Results.Auth;
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
            public Task<RegisterResult> Handle(Register request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
