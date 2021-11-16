using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using UI_Presentation.Models;

namespace TagHelperSamples.Bootstrap
{
    /// <summary>
    /// Panel Title Tag Helper
    /// </summary>
    [HtmlTargetElement("Search-Botton", ParentTag = "Search-Header")]
    public class AddSearchHeaderButtomTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var modalContext = (searchHeaderContext)context.Items[typeof(SearchHeaderTagHelper)];
            modalContext.SearchButton = childContent;
            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("Search-TextBox", ParentTag = "Search-Header")]
    public class AddSearchHeaderTextBoxTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {  
            output.PreContent.AppendFormat("");
            var childContent = await output.GetChildContentAsync();
            var searchTextBoxContent = new DefaultTagHelperContent();
            searchTextBoxContent.AppendHtml(childContent);
            var modalContext = (searchHeaderContext)context.Items[typeof(SearchHeaderTagHelper)];
            modalContext.SearchTextBox = searchTextBoxContent;
            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("tab-controlls", ParentTag = "tab-details-item")]
    public class TabControllsTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var tabDetailsContext = (TabDetailsItemContext)context.Items[typeof(TabDetailsItemTagHelper)];
            tabDetailsContext.TabControll = childContent;
            output.SuppressOutput();
        }
    }


    [HtmlTargetElement("tab-summery", ParentTag = "tab-details-item")]
    public class TabSummeryTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var tabContext = (TabDetailsItemContext)context.Items[typeof(TabDetailsItemTagHelper)];
            tabContext.TabSummery = childContent;
            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("customTag-children", ParentTag = "customChild-tag")]
    public class CustomTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var tabContext = (CustomeTagContext)context.Items[typeof(CustomChildTagHelper)];
            tabContext.children = childContent;
            output.SuppressOutput();
        }
    }
}
