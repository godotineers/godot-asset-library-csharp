using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public class EditAsset : IRequest<EditAssetResult>
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
