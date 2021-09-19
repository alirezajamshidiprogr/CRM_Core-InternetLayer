using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Threading.Tasks;
using UI_Presentation.Models;

namespace TagHelperSamples.Bootstrap
{
    /// <summary>
    /// Panel Tag Helper
    /// </summary>
    [RestrictChildren("Search-Botton" , "Search-TextBox")]
    public class SearchHeaderTagHelper : TagHelper
    {
        public PanelType Type { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var panelContext = new searchHeaderContext();
            context.Items.Add(typeof(SearchHeaderTagHelper), panelContext);

            await output.GetChildContentAsync();

            output.TagName = "div";
            

            // Default panel type will be panel-default
            output.Attributes.Add("class", "form-group");

            // panel title
            if (panelContext.SearchButton!= null)
            {
                var searchButtom = new TagBuilder("div");
                searchButtom.Attributes.Add("class", "searchButtom");
                searchButtom.InnerHtml.AppendHtml(panelContext.SearchButton);
                output.Content.AppendHtml(searchButtom);
            }

            if (panelContext.SearchTextBox != null)
            {
                output.Content.AppendHtml("<div class='searchTextBox'>");
                output.Content.AppendHtml(panelContext.SearchTextBox);
                output.Content.AppendHtml("</div>");
            }
        }
    }
}
