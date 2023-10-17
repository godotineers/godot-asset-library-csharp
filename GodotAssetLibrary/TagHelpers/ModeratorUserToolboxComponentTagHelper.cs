
using GodotAssetLibrary.Domain;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;

namespace GodotAssetLibrary.TagHelpers
{
    [HtmlTargetElement("moderator")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public class ModeratorToolboxComponentTagHelper : TagHelperComponentTagHelper
    {
        public ModeratorToolboxComponentTagHelper(
            ITagHelperComponentManager componentManager,
            ILoggerFactory loggerFactory) : base(componentManager, loggerFactory)
        {
        }
        // need to determine if a user is a moderator
        // if(User.IsInRole("moderator"))

    }

    public class ModeratorToolboxComponent : TagHelperComponent
    {
        public override int Order => 3;

        public override async Task ProcessAsync(TagHelperContext context,
                                                TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "moderator",
                    StringComparison.OrdinalIgnoreCase))
            {
                var content = await output.GetChildContentAsync();
                output.Content.SetHtmlContent(
                    $"<div>{content.GetContent()}</div>");
            }
        }
    }
}
