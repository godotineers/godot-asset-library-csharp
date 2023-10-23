using GodotAssetLibrary.TagHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        private class Filter : IActionFilter
        {
            public Filter(
                    IMediator mediator)
            {
                Mediator = mediator;
            }

            public IMediator Mediator { get; }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context.Controller is Controller contextController)
                {
                    contextController.ViewBag.Categories = new List<SelectListItem>().AddPlaceholder("Select...");
                    contextController.ViewBag.Licenses = new List<SelectListItem>().AddPlaceholder("Select...");
                    contextController.ViewBag.GodotVersions = new List<SelectListItem>().AddPlaceholder("Select...");
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
