using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    [HtmlTargetElement("modal-body", ParentTag = "modal")]
    public class ModalBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var modalContext = (ModalContext)context.Items[typeof(ModalTagHelper)];
            modalContext.Body = childContent;
            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("tab-Item", ParentTag = "tab-strip")]
    public class TabItemTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var tabContext = (TabStripContext)context.Items[typeof(TabStripTagHelper)];
            tabContext.Tab = childContent;
            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("tab-strip", ParentTag = "tab-strip")]
    public class TabBottonsTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var tabContext = (TabStripContext)context.Items[typeof(TabStripTagHelper)];
            tabContext.Tab = childContent;
            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("tab-details", ParentTag = "tab-strip")]
    public class TabDetailTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var tabContext = (TabStripContext)context.Items[typeof(TabStripTagHelper)];
            tabContext.TabDetails = childContent;
            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("tab-bottons", ParentTag = "tab-strip")]
    public class TabBottonTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var tabContext = (TabStripContext)context.Items[typeof(TabStripTagHelper)];
            tabContext.TabButtons = childContent;
            output.SuppressOutput();
        }
    }
}
