using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using UI_Presentation.Models;

namespace TagHelperSamples.Bootstrap
{
    public class CustomeTagContext
    {
        public IHtmlContent children { get; set; }
    }
    /// <summary>
    /// Panel Tag Helper
    /// </summary>
    [RestrictChildren("customTag-children")]
    [HtmlTargetElement("customChild-tag")]
    public class CustomChildTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string Style { get; set; }
        public string Class { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var customTagContext = new CustomeTagContext();
            context.Items.Add(typeof(CustomChildTagHelper), customTagContext);
            await output.GetChildContentAsync();
            output.Content.AppendHtml(string.Format("<div style='{0}' class='{1}' id='{2}'>", Style, Class, Id));

            if (customTagContext.children != null)
            {
                output.TagName = null;
                var children = customTagContext.children;
                var wirter = new System.IO.StringWriter();
                children.WriteTo(wirter, HtmlEncoder.Default);
                string childrenStringHtlm = wirter.ToString();
                string html = string.Empty;
                html += childrenStringHtlm;
                output.Content.AppendHtml(html);
            }
            output.Content.AppendHtml("</div>");
        }
    }
}
