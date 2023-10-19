using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public class GetAssetEdit : IRequest<GetAssetEditResult>
    {

        public class GetAssetEditHandler : IRequestHandler<GetAssetEdit, GetAssetEditResult>
        {
            public GetAssetEditHandler()
            {
            }

            public async Task<GetAssetEditResult> Handle(GetAssetEdit request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<GetAssetEditResult>(null);
            }
        }
    }
}
