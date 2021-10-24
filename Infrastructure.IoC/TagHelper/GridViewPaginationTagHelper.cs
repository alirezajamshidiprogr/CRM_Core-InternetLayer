using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
 
    [HtmlTargetElement("gridView-pagination", ParentTag = "grid-View")]
    [RestrictChildren("grid-pagination")]
    public class GridViewPaginationTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.AppendFormat(@"<div class='row'>");
            var childContent = await output.GetChildContentAsync();
            var paginationTagHelper = new DefaultTagHelperContent();
            paginationTagHelper.AppendFormat(@"");
            paginationTagHelper.AppendHtml(childContent);
            var modalContext = (GridViewContext)context.Items[typeof(GridViewTagHelper)];
            modalContext.Pagination = paginationTagHelper;
            output.SuppressOutput();
        }
    }
}
