using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public partial class EditAsset
    {
        public class EditAssetHandler : IRequestHandler<EditAsset, EditAssetResult>
        {
            public EditAssetHandler()
            {
            }

            public async Task<EditAssetResult> Handle(EditAsset request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<EditAssetResult>(null);
            }
        }
    }
}
