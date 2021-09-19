using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    public class GridViewContext
    {
        public IHtmlContent Header { get; set; }
        public IHtmlContent Body { get; set; }
        public IHtmlContent Footer { get; set; }
    }

    /// <summary>
    /// A Bootstrap modal dialog
    /// </summary>
    [RestrictChildren("gridView-header","gridView-body","gridView-footer")]
    public class GridViewTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string style { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var gridContext = new GridViewContext();
            context.Items.Add(typeof(GridViewTagHelper), gridContext);

            await output.GetChildContentAsync();

            var template = "<div class='parent'> <br /> <div class='header'>";

            output.TagName = "div";
            output.Attributes.SetAttribute("id", Id);

            output.Content.AppendHtml(template);
            
            if (gridContext.Header != null)
            {
                output.Content.AppendHtml(gridContext.Header);
            }
            output.Content.AppendHtml("</div>");
            if (gridContext.Body != null)
            {
                output.Content.AppendHtml("<div class='Gridbody'>");
                output.Content.AppendHtml(gridContext.Body);
                output.Content.AppendHtml("</div>");
            }

            if (gridContext.Footer != null)
            {
                output.Content.AppendHtml("<div class='GridFooter'>");
                output.Content.AppendHtml(gridContext.Footer);
                output.Content.AppendHtml("</div>");
            }
            output.Content.AppendHtml("</div>");
        }
    }
}
