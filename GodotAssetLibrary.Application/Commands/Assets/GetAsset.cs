using AutoMapper;
using GodotAssetLibrary.Application.Results.Assets;
using GodotAssetLibrary.DataLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.Application.Commands.Assets
{
    public class GetAssetById : IRequest<GetAssetResult>
    {
        public int AssetId { get; set; }

        public class GetAssetHandler : IRequestHandler<GetAssetById, GetAssetResult>
        {
            public GetAssetHandler(
                    IAssetLibraryContext libraryContext,
                    IMapper mapper
                )
            {
                LibraryContext = libraryContext;
                Mapper = mapper;
            }

            public IAssetLibraryContext LibraryContext { get; }
            public IMapper Mapper { get; }

            public async Task<GetAssetResult> Handle(GetAssetById request, CancellationToken cancellationToken)
            {
                var asset = await LibraryContext.Assets.FirstOrDefaultAsync(x => x.AssetId == request.AssetId);

                return Mapper.Map<GetAssetResult>(asset);
            }
        }
    }
}
