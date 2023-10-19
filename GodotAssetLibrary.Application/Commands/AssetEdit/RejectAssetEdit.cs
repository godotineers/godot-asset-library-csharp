using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public class RejectAssetEdit : IRequest<RejectAssetEditResult>
    {

        public class RejectAssetEditHandler : IRequestHandler<RejectAssetEdit, RejectAssetEditResult>
        {
            public RejectAssetEditHandler()
            {
            }

            public async Task<RejectAssetEditResult> Handle(RejectAssetEdit request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<RejectAssetEditResult>(null);
            }
        }
    }
}
