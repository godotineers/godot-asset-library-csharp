using GodotAssetLibrary.Application.Results.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class GetResetPassword : IRequest<GetResetPasswordResult>
    {

        public class GetResetPasswordHandler : IRequestHandler<GetResetPassword, GetResetPasswordResult>
        {
            public GetResetPasswordHandler()
            {
            }

            public async Task<GetResetPasswordResult> Handle(GetResetPassword request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<GetResetPasswordResult>(null);
            }
        }
    }
}
