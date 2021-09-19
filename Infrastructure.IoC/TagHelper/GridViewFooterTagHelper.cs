using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
 
    [HtmlTargetElement("gridView-footer", ParentTag = "grid-View")]
    [RestrictChildren("grid-footer-dataItem")]
    public class GridViewFooterTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.AppendFormat(@"<div class='GridFooter'>");
            var childContent = await output.GetChildContentAsync();
            var footerTagHelper = new DefaultTagHelperContent();
            footerTagHelper.AppendFormat(@"");
            footerTagHelper.AppendHtml(childContent);
            var modalContext = (GridViewContext)context.Items[typeof(GridViewTagHelper)];
            modalContext.Footer = footerTagHelper;
            output.SuppressOutput();
        }
    }
}
