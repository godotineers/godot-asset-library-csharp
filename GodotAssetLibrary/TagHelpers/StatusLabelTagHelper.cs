using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net;
using System.Text.Encodings.Web;

namespace GodotAssetLibrary.TagHelpers
{
    public class StatusLabelTagHelper : TagHelper
    {

        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.AddClass("label", HtmlEncoder.Default);
            var labelType = Value switch
            {
                "new" => "info",
                "in_review" => "primary",
                "rejected" => "danger",
                "accepted" => "success",
                _ => throw new InvalidDataException(),
            };
            output.AddClass($"label-{labelType}", HtmlEncoder.Default);
            output.Content.SetContent(WebUtility.HtmlEncode(Value.Replace("_", " ")));
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
