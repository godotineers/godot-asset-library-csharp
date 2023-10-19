using GodotAssetLibrary.Contracts;
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
            return ActivatorUtilities.CreateInstance<FrontendViewBindFilter>(serviceProvider, new object[] { ViewName });
        }

        private class FrontendViewBindFilter : IActionFilter
        {
            public FrontendViewBindFilter(
                    IRequestLifetime requestLifetime,
                    string viewName)
            {
                RequestLifetime = requestLifetime;
                ViewName = viewName;
            }

            public IRequestLifetime RequestLifetime { get; }
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
