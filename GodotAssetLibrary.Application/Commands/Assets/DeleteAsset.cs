using MediatR;

namespace GodotAssetLibrary.Application.Commands.Assets
{
    public class DeleteAsset : IRequest<string>
    {
        public int AssetId { get; set; }
    }
}
