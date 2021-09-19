using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    /// <summary>
    /// The modal-body portion of a Bootstrap modal dialog
    /// </summary>
    [HtmlTargetElement("gridView-header", ParentTag = "grid-View")]
    public class GridViewHeaderTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var modalContext = (GridViewContext)context.Items[typeof(GridViewTagHelper)];
            modalContext.Header = childContent;
            output.SuppressOutput();
        }
    }
}
