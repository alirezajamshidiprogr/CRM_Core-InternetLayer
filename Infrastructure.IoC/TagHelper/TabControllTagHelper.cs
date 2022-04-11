using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    public class TabControllContext
    {
        public IHtmlContent Items { get; set; }
    }

    //[HtmlTargetElement("tab-controll")]
    //[RestrictChildren("tabControll-Items")]
    //public class TabControllTagHelper : TagHelper
    //{
    //    public string Id { get; set; }
    //    public string Style { get; set; }

    //    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    //    {
    //        var tabContext = new TabControllContext();
    //        context.Items.Add(typeof(TabControllTagHelper), tabContext);

    //        await output.GetChildContentAsync();

    //        var template = $@"<ul class='nav nav-tabs mb-2' role='tablist'>";

    //        output.TagName = "ul";
    //        output.Attributes.SetAttribute("id", Id);

    //        output.Content.AppendHtml(template);
    //        if (tabContext.Items != null)
    //        {
    //            string html = "<li class='nav-item current'>";
    //            html += "<a class='nav-link d-flex align-items-center active' id='account-tab' role='tab' aria-selected='true' aria-controls='account' href='#PeopleInfo' data-toggle='tab'>";
    //            html += "<i class='bx bx-user mr-25'></i><span class='d-none d-sm-block'>@UI_Presentation.wwwroot.Resources.People.Title.PeopleInfo</span></a></li >";

    //            output.Content.AppendHtml(string.Format(html));
    //            output.Content.AppendHtml(tabContext.Items);

    //            //output.Content.AppendHtml(tabContext.Items);
    //        }

    //        output.Content.AppendHtml("</ul>");
    //    }
    //}

    //[HtmlTargetElement("mypager", Attributes = "total-pages, current-page, link-url")]
    //public class PagerAlirezaTagHelper : TagHelper
    //{
    //    public override void Process(TagHelperContext context, TagHelperOutput output)
    //    {
    //        if (
    //            int.TryParse(context.AllAttributes["total-pages"].Value.ToString(), out int totalPages) &&
    //            int.TryParse(context.AllAttributes["current-page"].Value.ToString(), out int currentPage))
    //        {
    //            var url = context.AllAttributes["link-url"].Value;
    //            output.TagName = "div";
    //            output.PreContent.SetHtmlContent(@"<ul class=""pagination"">");
    //            for (var i = 1; i <= totalPages; i++)
    //            {
    //                output.Content.AppendHtml($@"<li class=""{(i == currentPage ? "active" : "")}""><a href=""{url}?page={i}""  title=""Click to go to page {i}"">{ i}</a></li>");
    //            }
    //            output.PostContent.SetHtmlContent("</ul>");
    //            output.Attributes.Clear();
    //        }
    //    }
    //}

}
