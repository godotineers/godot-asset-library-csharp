using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public class SubmitAssetEdit : IRequest<SubmitAssetEditResult>
    {

        public class SubmitAssetEditHandler : IRequestHandler<SubmitAssetEdit, SubmitAssetEditResult>
        {
            public SubmitAssetEditHandler()
            {
            }

            public async Task<SubmitAssetEditResult> Handle(SubmitAssetEdit request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<SubmitAssetEditResult>(null);
            }
        }
    }
}
