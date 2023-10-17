using GodotAssetLibrary.Application.Results.Auth;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class Configure : IRequest<ConfigureResult>
    {
        public string Type { get; set; }
        public string Session { get; set; }

        public class ConfigureHandler : IRequestHandler<Configure, ConfigureResult>
        {
            public ConfigureHandler()
            {
            }

            public async Task<ConfigureResult> Handle(Configure request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<ConfigureResult>(null);
            }
        }
    }
}
