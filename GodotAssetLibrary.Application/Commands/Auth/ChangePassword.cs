using GodotAssetLibrary.Application.Results.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class ChangePassword : IRequest<ChangePasswordResult>
    {

        public class ChangePasswordHandler : IRequestHandler<ChangePassword, ChangePasswordResult>
        {
            public ChangePasswordHandler()
            {
            }

            public async Task<ChangePasswordResult> Handle(ChangePassword request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<ChangePasswordResult>(null);
            }
        }
    }
}
