using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    public class ModalContext
    {
        public IHtmlContent Body { get; set; }
        public IHtmlContent Footer { get; set; }
    }

    [RestrictChildren("modal-body", "modal-footer")]
    public class ModalTagHelper : TagHelper
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Bodystyle { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var modalContext = new ModalContext();
            context.Items.Add(typeof(ModalTagHelper), modalContext);

            await output.GetChildContentAsync();

            var template =
    $@"<div class='modal-dialog' role='document' style='width:1150px;'>
    <div class='modal-content' style='width: 247%;margin-right:-400px;overflow:auto;'>
     <button type = 'button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
        <h4 class='modal-title' id='{context.UniqueId}Label'>{Title}</h4>
      </div>
        <div class='modal-body' style='"+ Bodystyle + ";background:#f7f7f7;width:247%;margin-right:-70%;'>";

            output.TagName = "div";
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("id", Id);
            output.Attributes.SetAttribute("aria-labelledby", $"{context.UniqueId}Label");
            output.Attributes.SetAttribute("tabindex", "-1");
            var classNames = "modal fade";
            if (output.Attributes.ContainsName("class"))
            {
                classNames = string.Format("{0} {1}", output.Attributes["class"].Value, classNames);
            }
            output.Attributes.SetAttribute("class", classNames);
            output.Content.AppendHtml(template);
            if (modalContext.Body != null)
            {
                output.Content.AppendHtml(modalContext.Body);
            }
            output.Content.AppendHtml("</div>");
            if (modalContext.Footer != null)
            {
                output.Content.AppendHtml("<div class='modal-footer'>");
                output.Content.AppendHtml(modalContext.Footer);
                output.Content.AppendHtml("</div>");
            }
            
            output.Content.AppendHtml("</div></div>");
        }
    }
}
