using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public class ReviewAssetEdit : IRequest<ReviewAssetEditResult>
    {

        public class ReviewAssetEditHandler : IRequestHandler<ReviewAssetEdit, ReviewAssetEditResult>
        {
            public ReviewAssetEditHandler()
            {
            }

            public async Task<ReviewAssetEditResult> Handle(ReviewAssetEdit request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<ReviewAssetEditResult>(null);
            }
        }
    }
}
