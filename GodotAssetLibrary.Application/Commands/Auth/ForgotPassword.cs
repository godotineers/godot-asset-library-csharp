using GodotAssetLibrary.Application.Results.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class ForgotPassword : IRequest<ForgotPasswordResult>
    {

        public class ForgotPasswordHandler : IRequestHandler<ForgotPassword, ForgotPasswordResult>
        {
            public ForgotPasswordHandler()
            {
            }

            public async Task<ForgotPasswordResult> Handle(ForgotPassword request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<ForgotPasswordResult>(null);
            }
        }
    }
}
