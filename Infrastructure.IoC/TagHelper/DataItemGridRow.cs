using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Threading.Tasks;
using UI_Presentation.Models;

namespace TagHelperSamples.Bootstrap
{
    public static class getWidth
    {
        public static int lenIndex = 0;
        public static int lastIndex = 0;
        public static int indexHeader = 0;
        public static int indexRowsData = 0;
        public static int[] width = new int[30];
    }

    [HtmlTargetElement("header-gridview")]
    public class headerGridViewTagHelper : TagHelper
    {
        public string headerFieldName { get; set; }
        public int width { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //getWidth.lastIndex = 0;

            //if (getWidth.indexHeader == getWidth.lenIndex)
            //    getWidth.indexHeader = 0;

            //getWidth.width[getWidth.indexHeader] = width;
            //getWidth.indexHeader += 1;
            //getWidth.lastIndex = getWidth.indexHeader;

            string html = string.Empty;
            html += string.Format("<div class='headerFields' style='width:{0};'>{1}</div>", width == 0 ? "auto" : width + "px;", headerFieldName);
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }


    [HtmlTargetElement("grid-rows-data")]
    public class GridRowsDataTagHelper : TagHelper
    {
        public string FieldValue { get; set; }
        public bool visible { get; set; } = true;
        public int width { get; set; }
        public string Id { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //if (getWidth.indexRowsData >= getWidth.lastIndex+1)
            //    getWidth.indexRowsData = 0;

            //int with = getWidth.width[getWidth.indexRowsData];
            string style = string.Empty;
            style = visible ? "Display:" + " " + ";" : "Display:none;";
            style += "width:" + width + "px;";
            //if (visible)
            //{
            //    getWidth.indexRowsData += 1;
            //    style = visible ? "Display:" + " " + ";" : "Display:none;";
            //    style += "width:" + with + "px;";
            //}
            string html = string.Format("<div class='items' style='{0}'> <span  class='{1}' id='{2}'> {3} </span> </div>", style, visible ? "" : "specialFields",Id,FieldValue);

            output.TagMode = TagMode.StartTagAndEndTag;
            if (!visible)
                output.Attributes.Add("class", "specialTd");

            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("grid-rows-buttons")]
    public class GridRowsuttonsTagHelper : TagHelper
    {
        public bool IsEditMode { get; set; }
        public bool IsSelectMode { get; set; }
        public bool IsPrintMode { get; set; }
        public bool DeleteButton { get; set; } = true;
        public bool EditButton { get; set; } = true;
        public bool PringButton { get; set; }
        public string OnDeleteButtonAction { get; set; }
        public string OnEditButtonAction { get; set; }
        public string OnPrintButtonAction { get; set; }
        public string Style { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<div style='{0}'>", "float:right;" + Style);
            if (IsEditMode)
            {
                if (EditButton)
                {
                    html += string.Format("<a class='btn btn-info btn-round btnAction' href='#' onclick='{0}''>", OnEditButtonAction);
                    html += "<i class='icon-note'></i> ویرایش <div class='paper-ripple'><div class='paper-ripple__background'></div><div class='paper-ripple__waves'></div></div></a>";
                }
                if (DeleteButton)
                {
                    html += string.Format("<a class='btn btn-danger btn-round btnAction' href='#' onclick='{0}''>", OnDeleteButtonAction);
                    html += "<i class='icon-trash'></i> حذف <div class='paper-ripple'><div class='paper-ripple__background'></div><div class='paper-ripple__waves'></div></div></a>";
                }
            }
            else if (IsSelectMode)
            {
                html += "<a class='btn btn-info btn-round btnAction' href='#' onclick='selectPeople()'>";
                html += "<i class='icon-check'></i> انتخاب <div class='paper-ripple'><div class='paper-ripple__background'></div><div class='paper-ripple__waves'></div></div></a>";
            }
            else
            {
                // print Mode 
            }

            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("grid-footer-dataItem")]
    public class GridFooterDataItemTagHelper : TagHelper
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            string html = string.Format("<div style='width:30%;float:right;'> <b> {0} : </b> &nbsp; <span> {1} </span>   </div>", Title, Value);
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

}
