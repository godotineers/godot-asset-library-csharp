using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public class AcceptAssetEdit : IRequest<AcceptAssetEditResult>
    {

        public class AcceptAssetEditHandler : IRequestHandler<AcceptAssetEdit, AcceptAssetEditResult>
        {
            public AcceptAssetEditHandler()
            {
            }

            public async Task<AcceptAssetEditResult> Handle(AcceptAssetEdit request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<AcceptAssetEditResult>(null);
            }
        }
    }
}
