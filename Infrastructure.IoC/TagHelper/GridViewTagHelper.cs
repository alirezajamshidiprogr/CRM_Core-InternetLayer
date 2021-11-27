using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    public class GridViewContext
    {
        public IHtmlContent Header { get; set; }
        public IHtmlContent Body { get; set; }
        public IHtmlContent Pagination { get; set; }
        public IHtmlContent Footer { get; set; }
    }

    /// <summary>
    /// A Bootstrap modal dialog
    /// </summary>
    [RestrictChildren("gridView-header","gridView-body","gridView-footer","gridView-pagination")]
    public class GridViewTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string style { get; set; }
        public int TotalRowsCount { get; set; }
        public string ActionName { get; set; }
        public int PageNumber { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var gridContext = new GridViewContext();
            context.Items.Add(typeof(GridViewTagHelper), gridContext);

            await output.GetChildContentAsync();

            var template = "<div class='parent'> <br />";

            output.TagName = "div";
            output.Attributes.SetAttribute("id", Id);

            output.Content.AppendHtml(template);
            
            output.Content.AppendHtml("</div>");
            if (gridContext.Body != null)
            {
                output.Content.AppendHtml(string.Format("<div class='Gridbody' style='{0}'>", style));
                output.Content.AppendHtml(gridContext.Body);
                output.Content.AppendHtml("</div>");
            }
            int minRecordNumber = 1;
            int maxRecordNumber = 10;
            if (PageNumber > 0)
            {
                minRecordNumber = PageNumber * 10 + 1;
                maxRecordNumber = minRecordNumber + 9;
            }

            if (gridContext.Pagination != null)
            {
                string styleFirstRecord = PageNumber == 0 ? "font-size:24px;width:31px;opacity:0.4 ;pointer-events:none;margin-left:5px;" : "font-size:24px;width: 31px;margin-left:5px;";
                string stylePreviousSecond = PageNumber == 0 ? "font-size:24px;width: 31px;opacity:0.4 ;pointer-events:none;" : "font-size:24px;width: 31px;";
                string styleLastRecord = (PageNumber + 1) * 10 >= TotalRowsCount ? "font-size:24px;width: 31px;opacity:0.4 ;pointer-events:none;" : "font-size:24px;width: 31px;";
                string styleNextSecond = (PageNumber + 1) * 10 >= TotalRowsCount ? "font-size:24px;width: 31px;opacity:0.4 ;pointer-events:none;" :"font-size:24px;width: 31px;";

                string html = string.Empty;
                html += @"<div style='margin-top:6px;' class='row'><div class='col-sm-3'><div class='dataTables_info' id='data-table_info' role='status' aria-live='polite'>";
                html += @"رکورد" + minRecordNumber + " تا " + maxRecordNumber + "</div></div>";
                html += string.Format(@"<div class='col-sm-6'><div class='col-sm-3' style='text-align: left;margin-right:143px;'>");
                html += string.Format("<button style='{0}' class='btn btn-danger' onclick='ChangingGridPage(\"" + "backward" + "\", \"" + ActionName + "\" ,\"" + PageNumber +"\", \""+ TotalRowsCount + "\")' ><i aria-hidden='true' class='fa fa-step-forward' style='margin-right: -9px;'></i></button>", styleFirstRecord);
                html += string.Format("<button style='{0}' class='btn btn-success' onclick='ChangingGridPage(\"" + "right" + "\", \"" + ActionName + "\" ,\"" + PageNumber + "\", \"" + TotalRowsCount + "\")'><i aria-hidden='true' class='fa fa-arrow-right' style='margin-right: -9px;'></i> </button> </div>", stylePreviousSecond);
                html += string.Format("<div class='col-sm-3'><button class='btn btn-success' style='{0}' onclick='ChangingGridPage(\"" + "left" + "\", \"" + ActionName + "\" ,\"" + PageNumber + "\", \"" + TotalRowsCount + "\")'><i class='fa fa-arrow-left' aria-hidden='true' style='margin-right: -9px;'></i> </button> ", styleLastRecord);
                html += string.Format("<button class='btn btn-danger' onclick='ChangingGridPage(\"" + "forward" + "\", \"" + ActionName + "\" ,\"" + PageNumber + "\", \"" + TotalRowsCount + "\")' style='{0}'><i aria-hidden='true' class='fa fa-step-backward' style='margin-right: -9px;'></i> </button></div>", styleNextSecond);
                html += @"</div><div class='col-sm-3' style= 'text-align:left;'><span> تعداد رکوردها: " + TotalRowsCount + "</span></div>";


                //html += string.Format("<button style='{0}' onclick='{1}'><i aria-hidden='true' class='fa fa-step-forward'></i></button>", styleFirstRecord,ActionFunction);
                //html += string.Format("<button style='{0}' onclick='{1}')'><i aria-hidden='true' class='fa fa-arrow-right' ></i> </button> </div>", stylePreviousSecond, ActionFunction);
                //html += string.Format("<div class='col-sm-4'><button style='{0}' onclick='{1}' ><i class='fa fa-arrow-left' aria-hidden='true'></i> </button> ", styleLastRecord, ActionFunction);
                //html += string.Format("<button style='{0}' onclick='{1}'><i aria-hidden='true' class='fa fa-step-backward'></i> </button></div></div>", styleNextSecond, ActionFunction);


                output.Content.AppendHtml(html);
                output.Content.AppendHtml(gridContext.Pagination);
                output.Content.AppendHtml("</div>");
            }

            if (gridContext.Footer != null)
            {
                output.Content.AppendHtml("<div class='GridFooter'>");
                output.Content.AppendHtml(gridContext.Footer);
                output.Content.AppendHtml("</div>");
            }
        }
    }
}
