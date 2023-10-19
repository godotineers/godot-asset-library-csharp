using GodotAssetLibrary.Application.Results.Assets;
using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.DataLayer.Services;
using GodotAssetLibrary.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace GodotAssetLibrary.Application.Commands.Assets
{
    public class GetAssets : IRequest<GetAssetsResult>
    {
        public int? Category { get; set; }
        public string? Cost { get; set; }
        public string? Filter { get; set; }
        public string? GodotVersion { get; set; }
        public int? MaxResults { get; set; }
        public int? Offset { get; set; }
        public int? Page { get; set; }
        public bool? Reverse { get; set; }
        public string? Sort { get; set; }
        public SupportLevel? Support { get; set; }
        public CategoryTypes? Type { get; set; }
        public string? User { get; set; }

        public class GetAssetsHandler : IRequestHandler<GetAssets, GetAssetsResult>
        {
            public GetAssetsHandler(
                        IAssetService assetService,
                        IHttpContextAccessor httpContextAccessor)
            {
                AssetService = assetService;
                HttpContextAccessor = httpContextAccessor;
            }

            public IAssetService AssetService { get; }
            public IHttpContextAccessor HttpContextAccessor { get; }

            public async Task<GetAssetsResult> Handle(GetAssets request, CancellationToken cancellationToken)
            {
                int? category = null;
                var filter = "%";
                var username = "%";
                var cost = "%";
                var orderColumn = "modify_date";
                var supportLevels = Array.Empty<SupportLevel>();
                var pageSize = 40;
                var maxPageSize = 500;
                var pageOffset = 0;
                var minGodotVersion = 0;
                var maxGodotVersion = 9999999;

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    filter = "%" + Regex.Replace(request.Filter.Trim(), "[\\p{P}]+", "%") + "%";
                }

                var assets = Array.Empty<Asset>(); // await this.AssetService.SearchAssets();
                var totalCount = await this.AssetService.SearchAssetsCount(request.Category, request.Type, supportLevels, request.User, request.Cost, minGodotVersion, maxGodotVersion, filter, cancellationToken);

                Console.WriteLine((bool)HttpContextAccessor.HttpContext.Items["isFrontend"]);

                return new GetAssetsResult
                {
                    Result = assets,
                    Page = pageOffset / pageSize,
                    Pages = (int)Math.Ceiling(totalCount / (decimal)pageSize),
                    PageLength = pageSize,
                    TotalItems = totalCount,
                };
            }
        }
    }
}
