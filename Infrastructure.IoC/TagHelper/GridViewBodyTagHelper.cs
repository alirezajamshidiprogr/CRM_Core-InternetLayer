using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
 
    [HtmlTargetElement("gridView-body", ParentTag = "grid-View")]
    public class GridViewBodyTagHelper : TagHelper
    {
        /// <summary>
        /// Whether or not to show a button to dismiss the dialog. 
        /// Default: <c>true</c>
        /// </summary>
        public bool ShowDismiss { get; set; } = true;

        /// <summary>
        /// The text to show on the Dismiss button
        /// Default: Cancel
        /// </summary>
        public string DismissText { get; set; } = "Cancel";


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.AppendFormat(@"<div class='Gridbody'>");
            var childContent = await output.GetChildContentAsync();
            var bodyTagHelper = new DefaultTagHelperContent();
            bodyTagHelper.AppendFormat(@"");
            bodyTagHelper.AppendHtml(childContent);
            var modalContext = (GridViewContext)context.Items[typeof(GridViewTagHelper)];
            modalContext.Body = bodyTagHelper;
            output.SuppressOutput();
        }
    }
}
