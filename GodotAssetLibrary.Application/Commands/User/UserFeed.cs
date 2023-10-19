using GodotAssetLibrary.Application.Results.User;
using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.DataLayer.Services;
using MediatR;
using System.Security.Principal;

namespace GodotAssetLibrary.Application.Commands.User
{
    public class UserFeed : IRequest<UserFeedResult>
    {
        public int? Page { get; set; }
        public int? PageOffset { get; set; }
        public int? PageSize { get; set; }
        public int? MaxResults { get; set; }

        public class UserFeedHandler : IRequestHandler<UserFeed, UserFeedResult>
        {
            public UserFeedHandler(
                        IAssetEditService assetEditService,
                        ISessionUtility sessionUtility)
            {
                AssetEditService = assetEditService;
                SessionUtility = sessionUtility;
            }

            public IAssetEditService AssetEditService { get; }
            public ISessionUtility SessionUtility { get; }
            public IPrincipal Principal { get; }

            public async Task<UserFeedResult> Handle(UserFeed request, CancellationToken cancellationToken)
            {
                int pageSize = 40;
                int maxPageSize = 500;
                int pageOffset = 0;

                if (request.MaxResults.HasValue)
                {
                    pageSize = Math.Min(Math.Abs(request.MaxResults.Value), maxPageSize);
                }

                if (request.Page.HasValue)
                {
                    pageOffset = Math.Abs(request.Page.Value) * pageSize;
                }
                else if (request.PageOffset.HasValue)
                {
                    pageOffset = Math.Abs(request.PageOffset.Value);
                }

                var userData = SessionUtility.GetUserData();

                var events = await AssetEditService.ListEditEvents(userData.UserId, pageSize, pageOffset, cancellationToken);

                return new UserFeedResult
                {
                    Events = events,
                };
            }
        }
    }
}
