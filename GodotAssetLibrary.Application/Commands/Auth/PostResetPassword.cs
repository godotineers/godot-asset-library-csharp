using GodotAssetLibrary.Application.Results.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class PostResetPassword : IRequest<PostResetPasswordResult>
    {

        public class PostResetPasswordHandler : IRequestHandler<PostResetPassword, PostResetPasswordResult>
        {
            public PostResetPasswordHandler()
            {
            }

            public async Task<PostResetPasswordResult> Handle(PostResetPassword request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<PostResetPasswordResult>(null);
            }
        }
    }
}
