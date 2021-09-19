using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Threading.Tasks;
using UI_Presentation.Models;

namespace TagHelperSamples.Bootstrap
{

    [HtmlTargetElement("grid-rows-data")]
    public class GridRowsDataTagHelper : TagHelper
    {
        public string FieldValue { get; set; }
        public bool visible { get; set; } = true;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            string html = string.Format("<div class='items' style='Display:{0}'> <span  class='{2}'> {1} </span> </div>", visible ? "Block" : "none", FieldValue, visible ? "" : "specialFields" );

            output.TagMode = TagMode.StartTagAndEndTag;
            if(!visible)
              output.Attributes.Add("class", "specialTd");

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

            string html = string.Format("<div style='width:30%;float:right;'> <b> {0} : </b> &nbsp; <span> {1} </span>   </div>", Title ,Value);
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

}
