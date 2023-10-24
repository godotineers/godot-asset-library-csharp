using GodotAssetLibrary.Application.Results.AssetEdit;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.AssetEdit
{
    public partial class AcceptAssetEdit
    {
        public class Handler : IRequestHandler<AcceptAssetEdit, AcceptAssetEditResult>
        {
            public Handler()
            {
            }

            public async Task<AcceptAssetEditResult> Handle(AcceptAssetEdit request, CancellationToken cancellationToken)
            {
                return await Task.FromResult<AcceptAssetEditResult>(null);
            }
        }
    }
}
