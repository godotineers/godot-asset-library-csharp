using GodotAssetLibrary.Application.Results.Assets;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Assets
{
    public class CreateAsset : IRequest<CreateAssetResult>
    {
        // Previews
        public IEnumerable<CreateAssetPreview>? Previews { get; set; }

        public string? DownloadProvider { get; set; }
        public string? DownloadCommit { get; set; }
        public string? BrowseUrl { get; set; }
        public string? IconUrl { get; set; }
        public string? IssuesUrl { get; set; }
        public string? GodotVersion { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string VersionString { get; set; }
        public string Cost { get; set; }

        public class CreateAssetHandler : IRequestHandler<CreateAsset, CreateAssetResult>
        {
            public CreateAssetHandler()
            {
            }

            public async Task<CreateAssetResult> Handle(CreateAsset request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(new CreateAssetResult { });
            }
        }

        public class CreateAssetPreview
        {
            public bool? Enabled { get; set; }

            public string Type { get; set; }

            public string Link { get; set; }

            public string Thumbnail { get; set; }
        }
    }
}
