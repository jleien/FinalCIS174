using Microsoft.AspNetCore.Razor.TagHelpers;
namespace FinalCIS174.Controllers
{
    public class SubmitButtonTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("type", "submit");

            string newClasses = "btn btn-primary";
            string oldClasses = output.Attributes["class"]?.Value?.ToString();
            string classes = (string.IsNullOrEmpty(oldClasses)) ? newClasses : $"{oldClasses} {newClasses}";
            output.Attributes.SetAttribute("class", classes);
        }
    }
}
