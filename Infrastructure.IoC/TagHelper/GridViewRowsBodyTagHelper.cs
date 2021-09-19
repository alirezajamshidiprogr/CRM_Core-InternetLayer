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
    [RestrictChildren("grid-rows-data")]
    public class GridRowsBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var gridRowsBodycontext = new gridRowsBodyContext();
            context.Items.Add(typeof(SearchHeaderTagHelper), gridRowsBodycontext);
            await output.GetChildContentAsync();
            output.TagName = "div";
            // Default panel type will be panel-default
            output.Attributes.Add("class", "divRow");
        }
    }

}
