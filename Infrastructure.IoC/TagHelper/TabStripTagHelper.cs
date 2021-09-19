using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    public class TabStripContext
    {
        public IHtmlContent Tab { get; set; }
        public IHtmlContent TabDetails { get; set; }
        public IHtmlContent TabButtons { get; set; }
    }

    /// <summary>
    /// A Bootstrap modal dialog
    /// </summary>
    [HtmlTargetElement("tab-strip")]
    [RestrictChildren("tab-Item","tab-details","tab-bottons")]
    public class TabStripTagHelper : TagHelper
    {
        public bool HasConfirmContinueBotton { get; set; } = false;
        public string FormName { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tabStripContext = new TabStripContext();
            context.Items.Add(typeof(TabStripTagHelper), tabStripContext);

            await output.GetChildContentAsync();

            var template = "<div class='col-lg-12 detailFields'>";
            output.TagName = "div";
            output.Content.AppendHtml(template);

            if (tabStripContext.Tab != null)
            {
                output.Content.AppendHtml("<ul class='nav nav-tabs nav-rtl'>");
                output.Content.AppendHtml(tabStripContext.Tab);
                output.Content.AppendHtml("</ul>");
            }

            if (tabStripContext.TabDetails != null)
            {
                string html = $@"<div class='divformControl' style='margin-top:20px;'>
                              <div class='tab-content'>";
                output.Content.AppendHtml(html);
                output.Content.AppendHtml(tabStripContext.TabDetails);
                output.Content.AppendHtml("</div>");
            }

            if (tabStripContext.TabButtons != null)
            {
                string html = string.Empty;
                html += "<table style='clear: both;' id='formButtons' class='tblButons'>";
                html += "<tr>";
                html += string.Format("<td><button type ='button' id='{0}' onclick='{1}' class='btn btn-primary'>افـزودن</button></td>", "btnConfirm" + FormName, "btnAddEdit" + FormName + "()");

                if (HasConfirmContinueBotton)
                    html += string.Format("<td><button type = 'button' id='{0}' onclick='{1}' class='btn btn-primary'>افـزودن و ادامه</button></td>", "btnConfirmContinue" + FormName, "btnAddEdit" + FormName + "(true)");

                html += string.Format("<td><button type = 'button' id='{0}' onclick='{1}' class='btn btn-danger'>لغـو</button></td>", "btnCancel" + FormName, "btnClose" + FormName + "()");
                html += "</tr>";
                html += "</table>";

                output.Content.AppendHtml(html);
            }

            output.Content.AppendHtml("</div>");
        }
    }
}
