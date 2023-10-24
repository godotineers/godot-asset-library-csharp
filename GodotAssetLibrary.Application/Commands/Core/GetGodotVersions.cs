using GodotAssetLibrary.Application.Attributes;
using GodotAssetLibrary.Application.Results.Core;
using GodotAssetLibrary.Contracts.Repositories;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Core
{
    [UseCache]
    public class GetGodotVersions : IRequest<GetGodotVersionsResult>
    {
        public class GetGodotVersionsHandler : IRequestHandler<GetGodotVersions, GetGodotVersionsResult>
        {
            public GetGodotVersionsHandler(
                    IVersionRepository versionRepository)
            {
                VersionRepository = versionRepository;
            }

            public IVersionRepository VersionRepository { get; }

            public async Task<GetGodotVersionsResult> Handle(GetGodotVersions request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(
                    new GetGodotVersionsResult
                    {
                        Versions = (await VersionRepository.GetVersions(cancellationToken)).ToList()
                    });
            }
        }
    }
}
