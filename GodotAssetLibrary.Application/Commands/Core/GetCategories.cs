using FluentValidation;
using GodotAssetLibrary.Application.Attributes;
using GodotAssetLibrary.Application.Results.Core;
using GodotAssetLibrary.DataLayer.Services;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Core
{
    [UseCache]
    public class GetCategories : IRequest<GetCategoriesResult>
    {

        public class GetCategoriesHandler : IRequestHandler<GetCategories, GetCategoriesResult>
        {
            public GetCategoriesHandler(
                        ICategoryService categoryService)
            {
                CategoryService = categoryService;
            }

            public ICategoryService CategoryService { get; }

            public async Task<GetCategoriesResult> Handle(GetCategories request, CancellationToken cancellationToken)
            {
                return new GetCategoriesResult
                {
                    Categories = (await CategoryService.GetCategoriesAsync(cancellationToken)).ToList(),
                };
            }
        }
    }
}
