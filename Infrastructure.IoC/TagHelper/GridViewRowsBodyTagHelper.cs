using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    public class gridRowsBodyContext
    {
        public IHtmlContent SearchButton { get; set; }
    }
    /// <summary>
    /// The modal-body portion of a Bootstrap modal dialog
    /// </summary>
    [RestrictChildren("grid-rows-data", "grid-rows-buttons", "header-gridview")]
    [HtmlTargetElement("grid-rows-body")]
    public class GridRowsBodyTagHelper : TagHelper
    {
        public bool isHeader { get; set; } =false ;
        public string width { get; set; }  
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var gridRowsBodycontext = new gridRowsBodyContext();
            context.Items.Add(typeof(SearchHeaderTagHelper), gridRowsBodycontext);
            await output.GetChildContentAsync();
            output.TagName = "div";
            // Default panel type will be panel-default
            output.Attributes.Add("class", isHeader ? "header" : "divRow");
            output.Attributes.Add("style", "width:" + width + "px; !important");
        }
    }
}
