using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace DbrauResultUI.Helpers
{
    [HtmlTargetElement(Attributes = "asp-active-route")]
    public class ActiveRouteTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = ViewContext.RouteData.Values["Controller"].ToString();
            var currentAction = ViewContext.RouteData.Values["Action"].ToString();

            var tagController = context.AllAttributes.FirstOrDefault(a => a.Name == "asp-controller").Value.ToString();
            var tagAction = context.AllAttributes.FirstOrDefault(a => a.Name == "asp-action").Value.ToString();

            var cssClass = context.AllAttributes.FirstOrDefault(a => a.Name == "asp-active-route").Value.ToString();

            if (currentController == tagController.ToString() && currentAction == tagAction.ToString())
            {
                output.Attributes.SetAttribute("class", cssClass);
            }
        }
    }
}
