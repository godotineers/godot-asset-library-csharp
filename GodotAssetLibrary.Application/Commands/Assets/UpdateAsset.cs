using GodotAssetLibrary.Application.Results.Assets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.Assets
{
    public class UpdateAsset : IRequest<UpdateAssetResult>
    {

        public class UpdateAssetHandler : IRequestHandler<UpdateAsset, UpdateAssetResult>
        {
            public UpdateAssetHandler()
            {
            }

            public async Task<UpdateAssetResult> Handle(UpdateAsset request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<UpdateAssetResult>(null);
            }
        }
    }
}
