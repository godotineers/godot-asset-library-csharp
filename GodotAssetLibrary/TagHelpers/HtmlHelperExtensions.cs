using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GodotAssetLibrary.TagHelpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent NavLink(this IHtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object? valueObject = null)
        {
            var itemBuilder = new TagBuilder("li");

            if (controllerName.EndsWith("Controller"))
            {
                controllerName = controllerName[..^10];
            }

            var actionDescriptor = htmlHelper.ViewContext.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor != null && string.Equals(controllerName, actionDescriptor.ControllerName) && string.Equals(actionName, actionDescriptor.ActionName))
            {
                itemBuilder.AddCssClass("active");
            }

            itemBuilder.InnerHtml.AppendHtml(htmlHelper.ActionLink(linkText, actionName, controllerName, valueObject));

            return itemBuilder;
        }

        public static IEnumerable<SelectListItem> AddPlaceholder(this IEnumerable<SelectListItem> items, string placeholder, bool disabled = false)
        {
            return new SelectListItem[]
            {
                new SelectListItem(placeholder, string.Empty, true, disabled),
            }.Concat(items);
        }
    }
}
