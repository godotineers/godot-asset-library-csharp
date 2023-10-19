using GodotAssetLibrary.Application.Results.Auth;
using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Common.User;
using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.DataLayer.Services;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class Configure : IRequest<ConfigureResult>
    {
        public CategoryTypes? Type { get; set; }
        public string? Session { get; set; }

        public class ConfigureHandler : IRequestHandler<Configure, ConfigureResult>
        {
            public ConfigureHandler(
                        ICategoryService categoryService,
                        ISessionUtility sessionUtility,
                        ITokenUtility tokenUtility)
            {
                CategoryService = categoryService;
                SessionUtility = sessionUtility;
                TokenUtility = tokenUtility;
            }

            public ICategoryService CategoryService { get; }
            public ISessionUtility SessionUtility { get; }
            public ITokenUtility TokenUtility { get; }

            public async Task<ConfigureResult> Handle(Configure request, CancellationToken cancellationToken)
            {
                // determine the type of assets we are fetching.
                var categoryType = request.Type ?? CategoryTypes.Addons;

                var categories = await this.CategoryService.ListCategoriesByType(categoryType, cancellationToken);

                if (!string.IsNullOrWhiteSpace(request.Session))
                {
                    var sessionId = SessionUtility.GenerateSessionId();
                    var token = TokenUtility.GenerateToken(new TokenData
                    {
                        Session = sessionId,
                    });

                    return new ConfigureResult
                    {
                        Categories = categories,
                        Token = token,
                        LoginUrl = $"/auth/login?{token}",
                    };
                }
                else
                {
                    return new ConfigureResult
                    {
                        Categories = categories,
                    };
                }
            }
        }
    }
}
