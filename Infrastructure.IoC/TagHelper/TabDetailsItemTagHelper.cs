using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using UI_Presentation.Models;

namespace TagHelperSamples.Bootstrap
{
    public class TabDetailsItemContext
    {
        public IHtmlContent TabControll { get; set; }
        public IHtmlContent TabSummery { get; set; }
    }
    /// <summary>
    /// Panel Tag Helper
    /// </summary>
    [RestrictChildren("tab-controlls", "tab-summery")]
    [HtmlTargetElement("tab-details-item")]
    public class TabDetailsItemTagHelper : TagHelper
    {
        public PanelType Type { get; set; }
        public string Id { get; set; }
        public bool IsAcitve { get; set; } = false;
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tabDetialsItemContext = new TabDetailsItemContext();
            context.Items.Add(typeof(TabDetailsItemTagHelper), tabDetialsItemContext);
            await output.GetChildContentAsync();
            output.Content.AppendHtml(string.Format("<div id='{0}' style='margin-top:63px;' class='{1}'>",Id, IsAcitve ? "tab-pane fade in active" : "tab-pane fade"));
          
            if (tabDetialsItemContext.TabControll!= null)
            {
                output.TagName = null;
                output.Content.AppendHtml(tabDetialsItemContext.TabControll);
            }

            if (tabDetialsItemContext.TabSummery != null)
            {
                output.TagName = null;
                var children = tabDetialsItemContext.TabSummery;
                var wirter = new System.IO.StringWriter();
                children.WriteTo(wirter, HtmlEncoder.Default);
                string childrenStringHtlm = wirter.ToString();
                string html = string.Empty; 
                html += @"<div class='row summaryAddEditForm'>";
                html += childrenStringHtlm;
                html += "</div>";
                output.Content.AppendHtml(html);
            }

            output.Content.AppendHtml("</div>");

        }
    }
}
