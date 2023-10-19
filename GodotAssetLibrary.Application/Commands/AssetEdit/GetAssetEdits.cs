using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public class GetAssetEdits : IRequest<GetAssetEditsResult>
    {

        public class GetAssetEditsHandler : IRequestHandler<GetAssetEdits, GetAssetEditsResult>
        {
            public GetAssetEditsHandler()
            {
            }

            public async Task<GetAssetEditsResult> Handle(GetAssetEdits request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<GetAssetEditsResult>(null);
            }
        }
    }
}
