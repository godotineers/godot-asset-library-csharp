using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class Logout : IRequest<LogoutResult>
    {

        public class LogoutHandler : IRequestHandler<Logout, LogoutResult>
        {
            public LogoutHandler()
            {
            }

            public async Task<LogoutResult> Handle(Logout request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<LogoutResult>(null);
            }
        }
    }
}
