using GodotAssetLibrary.Application.Commands.Core;
using GodotAssetLibrary.Commands;
using GodotAssetLibrary.Common.Domain;
using GodotAssetLibrary.Domain;
using GodotAssetLibrary.TagHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GodotAssetLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BindSelectItemsToViewBagAttribute : Attribute, IFilterFactory
    {
        public BindSelectItemsToViewBagAttribute()
        {
        }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return ActivatorUtilities.CreateInstance<Filter>(serviceProvider, new object[] { });
        }

        private class Filter : IAsyncActionFilter
        {
            public Filter(
                    IMediator mediator)
            {
                Mediator = mediator;
            }

            public IMediator Mediator { get; }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.Controller is Controller contextController)
                {
                    contextController.ViewBag.Categories = (await Mediator.Send(new CreateSelectListItems<Category>
                    {
                        Items = (await Mediator.Send(new GetCategories())).Categories,
                        LabelExpression = x => x.CategoryName,
                        ValueExpression = x => x.CategoryId,
                        GroupExpression = x => x.CategoryType,
                    })).AddPlaceholder("Select...", true);

                    contextController.ViewBag.Licenses = (await Mediator.Send(new CreateSelectListItems<SoftwareLicense>
                    {
                        Items = (await Mediator.Send(new GetLicenses())).Licenses,
                        LabelExpression = x => x.Name,
                        ValueExpression = x => x.Tag,
                    })).AddPlaceholder("Select...", true);

                    contextController.ViewBag.GodotVersions = (await Mediator.Send(new CreateSelectListItems<GodotVersion>
                    {
                        Items = (await Mediator.Send(new GetGodotVersions())).Versions,
                        LabelExpression = x => x.Tag,
                        ValueExpression = x => x.Tag,
                    })).AddPlaceholder("Select...", true);
                }

                await next();
            }
        }
    }
}
