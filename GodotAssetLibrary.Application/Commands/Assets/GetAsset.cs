using AutoMapper;
using FluentValidation;
using GodotAssetLibrary.Application.Results.Assets;
using GodotAssetLibrary.DataLayer.Services;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Assets
{
    public class GetAssetById : IRequest<GetAssetResult>
    {
        public int AssetId { get; set; }

        public class GetAssetHandler : IRequestHandler<GetAssetById, GetAssetResult>
        {
            public GetAssetHandler(
                    IAssetService assetService,
                    IMapper mapper
                )
            {
                AssetService = assetService;
                Mapper = mapper;
            }

            public IAssetService AssetService { get; }
            public IMapper Mapper { get; }

            public async Task<GetAssetResult> Handle(GetAssetById request, CancellationToken cancellationToken)
            {
                var asset = await AssetService.GetAssetById(request.AssetId);

                return Mapper.Map<GetAssetResult>(asset);
            }
        }

        public class Validator : AbstractValidator<GetAssetById>
        {
            public Validator()
            {
                RuleFor(x => x.AssetId).GreaterThan(0);
            }
        }
    }
}
