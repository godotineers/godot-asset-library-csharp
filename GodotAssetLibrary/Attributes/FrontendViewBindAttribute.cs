using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GodotAssetLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FrontendViewBindAttribute : Attribute, IFilterFactory
    {
        public string ViewName { get; set; }

        public bool IsReusable => false;


        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return ActivatorUtilities.CreateInstance<Filter>(serviceProvider, new object[] { ViewName });
        }

        private class Filter : IActionFilter
        {
            public Filter(
                    IRequestLifetime requestLifetime,
                    IMediator mediator,
                    string viewName)
            {
                RequestLifetime = requestLifetime;
                Mediator = mediator;
                ViewName = viewName;
            }

            public IRequestLifetime RequestLifetime { get; }
            public IMediator Mediator { get; }
            public string ViewName { get; }

            public void OnActionExecuting(ActionExecutingContext context)
            {
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (RequestLifetime.IsFrontend && context.Result is OkObjectResult okResult && context.Controller is Controller contextController)
                {
                    var viewResult = contextController.View(ViewName, okResult.Value);
                    context.Result = viewResult;
                }
            }
        }
    }
}
