
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagHelperSamples.Bootstrap;
using CRM_Core.Application.Interfaces;
using CRM_Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace UI_Presentation.Models
{
    [HtmlTargetElement("Icon-Button")]
    public class IconButton : TagHelper
    {
        public string onClickEvent { get; set; }
        public string buttomClass { get; set; }
        public int BottonType { get; set; }
        public string icon { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            if (BottonType == 1 )
            sb.AppendFormat("<button type='button' style='margin-left:7px;' onclick ={0} class ='btn btn-success' id='{1}'><i class='bx bx-check-circle' ></i>&nbsp;{4} </button>", "\"" + onClickEvent +"\"", Id , buttomClass, icon, Title);
            else if (BottonType == 2)
                sb.AppendFormat("<button type='button' style='margin-left:7px;' onclick ={0} class ='btn btn-danger' id='{1}'><i class='bx bxs-x-circle' ></i>&nbsp;{4} </button>", "\"" + onClickEvent + "\"", Id, buttomClass, icon, Title);
            else if (BottonType == 3)
                sb.AppendFormat("<button type='button' style='margin-left:7px;' onclick ={0} class ='btn btn-primary' id='{1}'><i class='bx bx-search-alt-2' ></i>&nbsp;{4} </button>", "\"" + onClickEvent + "\"", Id, buttomClass, icon, Title);
            else if (BottonType == 4)
                sb.AppendFormat("<button type='button' style='margin-left:7px;' onclick ={0} class ='btn btn-info' id='{1}' ><i class='bx bx-trash' ></i>&nbsp;{4} </button>", "\"" + onClickEvent + "\"", Id, buttomClass, icon, Title);
            else if (BottonType == 5)
                sb.AppendFormat("<button type='button' style='margin-left:7px;' onclick ={0} class ='btn btn-primary' id='{1}' ><i class='bx bxs-plus-circle' ></i>&nbsp;{4} </button>", "\"" + onClickEvent + "\"", Id, buttomClass, icon, Title);
            else if (BottonType == 6)
                sb.AppendFormat("<button type='button' style='margin-left:7px;' onclick ={0} class ='btn btn-danger' id='{1}'><i class='bx bxs-minus-circle' ></i>&nbsp;{4} </button>", "\"" + onClickEvent + "\"", Id, buttomClass, icon, Title);

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("short-TextBox")]
    public class shortTextBox : TagHelper
    {
        public string Id { get; set; }
        public string labelTitle { get; set; }
        public string RequeiredMessage { get; set; }
        public string onChangeTextBox { get; set; }
        public string value { get; set; }
        public string Style { get; set; }
        public string InputType { get; set; }
        public bool required { get; set; } = false;
        public string tabIndex { get; set; } = null;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<div class='form-group'> ", "");
            html += string.Format("<div class='controls'> ");
            html += string.Format("<label>{0}</label>", labelTitle);
            html += string.Format("<input id='{0}' name ='text' class='form-control text-left' dir='ltr' {1}  placeholder='{2}' {3} value='{4}' type='{5}' onchange='{6}' '{7}' style=''>", Id, required ? "required=\"" + string.Empty + "\"" : string.Empty, labelTitle, RequeiredMessage != string.Empty ? "data-validation-required-message=\"" + RequeiredMessage + "\"" : string.Empty, value != string.Empty ? value : string.Empty, InputType == string.Empty ? "text" : InputType, onChangeTextBox,tabIndex != null ? "tabindex=\"" + tabIndex + "\"" : "", Style);
            html += "</div>";
            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("TextBox-Botton")]
    public class TextBoxButton : TagHelper
    {
        public string ColClassSize { get; set; }
        public string labelTitle { get; set; }
        public string Id { get; set; }
        public string ButtonId { get; set; }
        public string ButtonEvent { get; set; }
        public string onChangeTextBox { get; set; }
        public string ButtonTitle { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<div class='{0}'>", ColClassSize + "form-group");
            html += "<div class='controls'>";
            html += "<label>" + labelTitle + "</label> <div>";
            html += string.Format("<input id='{0}' name='text' class='form-control text-left' dir='ltr' placeholder='{1}' value='' type='' onchange='' '' ='' style='width: 20%;float: right;border: 1px solid #605d5d;border-radius: 0px 10px 10px 0px;'>",Id,labelTitle);
            html += string.Format("<button type='button' style='margin-left:7px;background: #605d5d !important;border-radius: 12px 0px 0px 12px !important;' onclick='{0}' class='btn btn-primary' id='{1}'><i class='bx bx-search-alt-2'></i>&nbsp;{2}</button>",ButtonEvent, ButtonId, ButtonTitle);
            html += "</div></div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }


    [HtmlTargetElement("Row-Col")]
    public class RowCol : TagHelper
    {
        public List<int> Cols { get; set; }
        public List<string> Values { get; set; }
        public string Ids { get; set; }
        public string Style { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += "<div class='row'>";
            for (int i = 0; i < Cols.Count; i++)
            {
                html += string.Format("<div class='{0}'>", "col-lg-" + Cols[i].ToString());
                html += string.Format("<p style='{0}' id='{1}'> {2} </p></div>", Style, Ids != null  && i > 0  ? Ids : string.Empty, Values[i].ToString());
            }
            
            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }


    [HtmlTargetElement("DateTime-TextBox")]
    public class DateTimeTextBox : TagHelper
    {
        public string Id { get; set; }
        public string labelTitle { get; set; }
        public string RequeiredMessage { get; set; }
        public string value { get; set; }
        public string Style { get; set; }
        public bool required { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<div class='mb-1'>");
            html += string.Format("<label>{0}</label>", labelTitle);
            html += string.Format("<fieldset class='form-group position-relative has-icon-left'>");
            html += string.Format("<input id={0} type='text' class='form-control txtDate' placeholder='انتخاب تاریخ' value='{1}'>", Id, value);
            html += string.Format("<div class='form-control-position'> <i class='bx bx-calendar'></i></div></fieldset>");
            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("FormTitle")]
    public class FormTitle : TagHelper
    {
        public string Form_Title { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<h5 class='mb-1'>{0}</h5>", Form_Title);
            output.TagMode = TagMode.StartTagOnly;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }


    [HtmlTargetElement("Profile-Image")]
    public class ProfileImage : TagHelper
    {
        public string Id { get; set; }
        public string ProfileTitle { get; set; }
        public string ImageLink { get; set; }
        public string ImageAlt { get; set; }
        public string OnClickChange { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format(" <div class='media mb-2'>");
            html += string.Format("<a class='mr-2' href='#'> <img class='users-avatar-shadow rounded-circle' alt='{0}' src='{1}' width='120' height='120'></a>", ImageAlt, "../../assets/images/portrait/small/avatar-s-26.jpg");
            html += string.Format("<div class='media-body'>");
            html += string.Format(" <h4 class='media-heading'>{0}</h4>", ProfileTitle);
            html += string.Format("<div class='col-12 px-0 d-flex'>");
            html += string.Format("<a class='btn btn-sm btn-primary mr-25' href='#' onclick='{0}'>تغییر</a>", OnClickChange);
            html += "</div>";
            html += "</div>";
            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }



    [HtmlTargetElement("checkBox")]
    public class CheckBox : TagHelper
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Style { get; set; }
        public bool Value { get; set; } = false;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += "<div class='form-group d-flex flex-md-row flex-column justify-content-between align-items-center'>";
            html += "<div class='text-left'> <div class='checkbox checkbox-sm'>";
            html += string.Format("<input type = 'checkbox' class='form-check-input' id='{0}'>", Id);
            html += string.Format("<label class='checkboxsmall' for='{0}'><small style ='{1}' > {2} </small ></label > ", Id, Style, Title);
            html += "</div></div></div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("inputBox")]
    public class InputBox : TagHelper
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string PlaceHolder { get; set; }
        public string Style { get; set; }
        public string Value { get; set; } 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<div style='{2}'><input class='form-control' id='{0}' type='text' placeholder='{1}' aria-invalid='false'  value='{3}'></div>", Id,PlaceHolder,Style,Value) ;
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("select-Box")]
    public class SelectBox : TagHelper
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Style { get; set; }
        public string AllItemsName { get; set; }
        public string OnChangeEvent { get; set; }
        public List<SelectListItem> dropDownDataBound { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<div style='{0}'><select id='{1}' onchange={2} class='form-control' aria-invalid='false'>", Style, Id, "\"" + OnChangeEvent + "\"") ;

            if (!string.IsNullOrEmpty(AllItemsName))
                html += string.Format("<option value ='null'>{0}</option>", AllItemsName);
            if (dropDownDataBound != null)
            {
                foreach (var item in dropDownDataBound)
                {
                    if (item.Selected)
                        html += string.Format("<option value='{0}' selected>{1}</option>", item.Value, item.Text);
                    else
                        html += string.Format("<option value='{0}'>{1}</option>", item.Value, item.Text);
                }
            }

            html += "</select></div>";
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            output.TagName = null;
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("long-time-TextBox")]
    public class LongTimeTextBox : TagHelper
    {
        public string Id { get; set; }
        public string labelTitle { get; set; }
        public bool required { get; set; } = false;
        public string Value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += "<div class='tabFields' style='width:100%;text-align:right;'>";
            html += string.Format("<div style='float: right;'><label class='labelWidget clockpicker-autoclose' style='width:101px !important;'>{0}</label></div>", labelTitle);
            html += string.Format("<div class='input-group'><span class='input-group-addon'><i class='icon-clock'></i></span> <input style='width:155px !important;' id='{0}' placeholder='{1}' class='form-control clockpicker-autoclose' required='{2}' value='{3}'></div>", Id, labelTitle, required ? "required" : "", Value);
            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }


    //[HtmlTargetElement("checkBox")]
    //public class CheckBox : TagHelper
    //{
    //    public string Id { get; set; }
    //    public string Title { get; set; }
    //    public string ischecked { get; set; }
    //    public bool required { get; set; } = false;
    //    public override void Process(TagHelperContext context, TagHelperOutput output)
    //    {
    //        string html = string.Empty;
    //        html += string.Format("<div><label class='lblChkBox'><input id ='{0}' class='chkBoxFields' type='checkbox'>{1}</label></div>", Id, Title);

    //        output.TagMode = TagMode.StartTagAndEndTag;
    //        var sb = new StringBuilder();
    //        sb.AppendFormat(html);
    //        output.PreContent.SetHtmlContent(sb.ToString());
    //    }
    //}

    [HtmlTargetElement("linkIcon")]
    public class LinkIcon : TagHelper
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string OnClick { get; set; }
        public bool required { get; set; } = false;
        public string IconClass { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += "<div class='input-group'>";
            html += string.Format("<a href = '#' class='btn btn-success' style='margin-bottom:7px;' data-toggle='dropdown' onclick='{0}'>", OnClick);
            html += string.Format("<i class='{0}' style='padding:5px;'></i>{1}</a></div>", IconClass, Title);

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("textBox-Icon")]
    public class TextBoxIcon : TagHelper
    {
        public string Id { get; set; }
        public string Placeholder { get; set; }
        public int maxLength { get; set; }
        public bool required { get; set; } = false;
        public string IconClass { get; set; }
        public string Style { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<span class='input-group-addon'><i class='{0}'></i></span><input class='form-control' id='{1}' type='text' maxlength='{2}' placeholder='{3}' '{4}'  style='{5}'>", IconClass, Id, maxLength, Placeholder, required ? "required=required" : "", Style);

            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("time-TextBox")]
    public class TimeTextBox : TagHelper
    {
        public string Id { get; set; }
        public string labelTitle { get; set; }
        public bool required { get; set; } = false;
        public string Style { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<div class='col-lg-4 tabFields'style='{0}'>", Style);
            html += string.Format("<div style='width: 25%;float: right;'><label class='labelWidget clockpicker-autoclose' style='width:81px;'>{0}</label></div>", labelTitle);
            html += string.Format("<div class='input-group'><span class='input-group-addon'><i class='icon-clock'></i></span> <input id='{0}' placeholder='{1}' class='form-control clockpicker-autoclose' required='{2}'></div>", Id, labelTitle, required ? "required" : "");
            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("custom-tag")]
    public class CustomeTag : TagHelper
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Style { get; set; }
        public string Class { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<{0} style='{1}' class='{2}' id='{3}'>{4}", Name, Style, Class, Id, Value);
            html += string.Format("</{0}>", Name);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }


    //[HtmlTargetElement("dropDown")]
    //public class DropDown : TagHelper
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string AllItemsName { get; set; }
    //    public string labelTitle { get; set; }
    //    public string onchangeDropDown { get; set; }
    //    public List<SelectListItem> dropDownDataBound { get; set; }
    //    public bool isLongForm { get; set; } = false;
    //    public bool required { get; set; } = false;
    //    public int? width { get; set; } = null;
    //    public bool Enable { get; set; } = true;
    //    public string value { get; set; }
    //    public override void Process(TagHelperContext context, TagHelperOutput output)
    //    {
    //        string html = string.Empty;

    //        if (isLongForm)
    //        {
    //            string style = string.Empty;
    //            style += width != null ? "width:" + width + "px !important;float:right !important;" : "width:160px !important;float:right !important;";
    //            style += !Enable ? "opacity:0.5; pointer-events: none;" : "";

    //            html += "<div class='col-lg-4 tabFields'>";
    //            html += string.Format("<label class='labelWidget clockpicker-autoclose' style='width:101px !important;text-align:right;float:right;'>{0}</label>", labelTitle);
    //            html += string.Format("<select id='{0}' class='comboBox' name='{1}' onchange='{2}' style='{3}'>", Id, Name, onchangeDropDown, style);
    //        }
    //        else
    //        {
    //            string style = string.Empty;
    //            style += !Enable ? "opacity:0.5; pointer-events: none;" : "";

    //            html += "<div class='tabFields'>";
    //            html += string.Format("<label class='labelWidget clockpicker-autoclose' style='width:81px;'>{0}</label>", labelTitle);
    //            html += string.Format("<select id='{0}' class='comboBox' name='{1}' onchange='{2}'> style='{3}' ", Id, Name, onchangeDropDown, style);
    //        }

    //        if (!string.IsNullOrEmpty(AllItemsName))
    //            html += string.Format("<option value ='null'>{0}</option>", AllItemsName);
    //        if (dropDownDataBound != null)
    //        {
    //            foreach (var item in dropDownDataBound)
    //            {
    //                if (item.Selected)
    //                    html += string.Format("<option value='{0}' selected>{1}</option>", item.Value, item.Text);
    //                else
    //                    html += string.Format("<option value='{0}'>{1}</option>", item.Value, item.Text);
    //            }
    //        }

    //        html += "</select>";
    //        html += "</div>";

    //        output.TagMode = TagMode.StartTagAndEndTag;
    //        var sb = new StringBuilder();
    //        sb.AppendFormat(html);
    //        output.PreContent.SetHtmlContent(sb.ToString());
    //    }
    //}

    [HtmlTargetElement("dropDown")]
    public class DropDown : TagHelper
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AllItemsName { get; set; }
        public string labelTitle { get; set; }
        public string onchangeDropDown { get; set; }
        public List<SelectListItem> dropDownDataBound { get; set; }
        public bool required { get; set; } = false;
        public bool Enable { get; set; } = true;
        public string value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            string style = string.Empty;

            html += "<div class='form-group'>";
            html += string.Format("<label class='labelWidget clockpicker-autoclose' style='width:101px !important;text-align:right;float:right;'>{0}</label>", labelTitle);
            html += string.Format("<select class='form-control' id='{0}' name='{1}' onchange='{2}' >", Id, Name, onchangeDropDown);


            if (!string.IsNullOrEmpty(AllItemsName))
                html += string.Format("<option value ='null'>{0}</option>", AllItemsName);
            if (dropDownDataBound != null)
            {
                foreach (var item in dropDownDataBound)
                {
                    if (item.Selected)
                        html += string.Format("<option value='{0}' selected>{1}</option>", item.Value, item.Text);
                    else
                        html += string.Format("<option value='{0}'>{1}</option>", item.Value, item.Text);
                }
            }

            html += "</select>";
            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("modal-buttom")]
    public class ModalOpen : TagHelper
    {
        public string Title { get; set; }
        public string ModalId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            string html = string.Empty;
            html += string.Format(@"<a class='btn btn-info btn-round' style='background:#4a6060 !important;color:white;' data-target='{0}' data-toggle='modal'>
                <i class='bx bx-search'></i>
                {1}
                <div class='paper-ripple'><div class='paper-ripple__background' style='opacity: 0;'></div><div class='paper-ripple__waves'></div></div>
            </a>", "#" + ModalId, Title);

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("long-TextBox")]
    public class TextArea : TagHelper
    {
        public string Id { get; set; }
        public string labelTitle { get; set; }
        public string width { get; set; }
        public string value { get; set; }
        public bool isDateType { get; set; } = false;
        public bool required { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            string html = string.Empty;
            html += "<div class='form-group' style='clear:both;width:100%;'>";
            html += string.Format("<label class='labelWidget' style='width:101px;'> {0} </label>", labelTitle);
            html += string.Format("<input id='{0}' placeholder='{1}' type='{2}' style='width:{3}' value='{4}' class='{5}' {6}/> ", Id, labelTitle, "text", width == string.Empty ? "auto !important" : width + "!important", value == string.Empty ? "" : value, isDateType ? "form-control TextArea txtDate" : "form-control TextArea", required ? "required=required" : "");
            html += "</div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("tab-summeryItems")]
    public class tabSummeryItem : TagHelper
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public string Id { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += "<div class='col-lg-4' style='margin-right:123px;font-size:1.1em;color: #042b55;'>";
            html += string.Format("<p style='float:right;'> {0} : </p>", Title);
            html += string.Format("<p id={0}> {1} </p>", Id, Value);
            html += "</div>";
            output.TagName = null;
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("form-buttons")]
    public class FormButtons : TagHelper
    {
        public bool HasConfirmContinueBotton { get; set; }
        public string FormName { get; set; }
        public string ConfirmButtonTitle { get; set; }
        public string OnAddButtonCick { get; set; }
        public string OnAddButtonContinueCick { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string eventBtnConfirm = OnAddButtonCick != null ? OnAddButtonCick : "btnAddEdit" + FormName + "()";
            string eventBtnConfirmContinue = OnAddButtonContinueCick != null ? OnAddButtonContinueCick : "btnAddEdit" + FormName + "(true)";
            string html = string.Empty;
            html += "<table id='formButtons' class='tblButons'>";
            html += "<tr>";
            html += string.Format("<td><button type ='button' id='{0}' onclick='{1}' class='btn btn-primary fa fa-check'>{2}</button></td>", "btnConfirm" + FormName, eventBtnConfirm, ConfirmButtonTitle == string.Empty ? "ثبت" : ConfirmButtonTitle);

            if (HasConfirmContinueBotton)
                html += string.Format("<td><button type = 'button' id='{0}' onclick='{1}' class='btn btn-primary'>ثبت و ادامه</button></td>", "btnConfirmContinue" + FormName, eventBtnConfirmContinue);

            html += string.Format("<td><button type = 'button' id='{0}' onclick='{1}' class='btn btn-danger fa fa-close (alias)'>لغـو</button></td>", "btnCancel" + FormName, "btnClose" + FormName + "()");
            html += "</tr>";
            html += "</table>";

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("Text-Area")]
    public class MultiLineTextArea : TagHelper
    {
        public string Id { get; set; }
        public string labelTitle { get; set; }
        public string value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            string html = string.Empty;
            html += "<div class='form-group' style='width:100% !important;clear:both;'>";
            html += string.Format("<label class='labelWidget'> {0} </label>", labelTitle);
            html += string.Format("<textarea id='{0}' placeholder='{1}' class='form-control TextArea' style='width: 96% !important'>{2}", Id, labelTitle, value != string.Empty ? value : string.Empty);
            html += "</textarea></div>";

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("Text-Address")]
    public class TextAddress : TagHelper
    {
        public string provinceValue { get; set; }
        public string cityValue { get; set; }
        public string areaValue { get; set; }
        public string streetValue { get; set; }
        public string alleyValue { get; set; }
        public string OtherAddress { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            string html = string.Empty;
            html += "<div class='form-group' style='width:100% !important;clear:both;'>";
            html += string.Format("<div style='float:right;margin-left:6px;'><input id='txtProvince' value='{0}' class='form-control' style = 'width:118px!important' placeholder='استان'></div>", provinceValue != string.Empty ? provinceValue : string.Empty);
            html += string.Format("<div style='float:right;margin-left:6px;'><input id='txtCity' value='{0}' class='form-control' style = 'width:118px!important' placeholder='شـهر'></div>", cityValue != string.Empty ? cityValue : string.Empty);
            html += string.Format("<div style='float:right;margin-left:6px;'><input id='txtArea' value='{0}' class='form-control' style = 'width:118px!important' placeholder='منـطقه'></div>", areaValue != string.Empty ? areaValue : string.Empty);
            html += string.Format("<div style='float:right;margin-left:6px;'><input id='txtStreet' value='{0}' class='form-control' style = 'width:200px !important' placeholder='خیـابان'></div>", streetValue != string.Empty ? streetValue : string.Empty);
            html += string.Format("<div style='float:right;margin-left:6px;'><input id='txtAlley' value='{0}' class='form-control' style = 'width:152px !important' placeholder='کوچه'> </div>", alleyValue != string.Empty ? alleyValue : string.Empty);
            html += string.Format("<br /><div style='margin-top:26px;'><input id='txtOtherAddress' value='{0}' class='form-control' style = 'width:97% !important' placeholder='ادامه آدرس'> </div>", OtherAddress != string.Empty ? OtherAddress : string.Empty);
            html += "</div>";
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("grid-headerFields")]
    public class GridItems : TagHelper
    {
        public string headerFieldName { get; set; }
        public int width { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<div class='headerFields' style='width:{0};'>{1}</div>", width == 0 ? "auto" : width + "px;", headerFieldName);
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("Search-Grid-TextBox")]
    public class SearchGridTextBox : TagHelper
    {
        public string Placeholder { get; set; }
        public string OnClickAction { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format(@"<div class='input-group'>
                                  <input id='txt-search' PlaceHolder='{0}' type='text' style='float: left;height:47px; width:100%;text-align:center;'>
                                  <span onclick={1} class='input-group-addon' style='background: #d94f4f00  ! important;'>
                                   <i class='btn btn-success icon-magnifierr' style='background: #514d4d;'>&nbsp; جستجو</i>
                                   </span>
                                   </div>", Placeholder, OnClickAction);

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("file-browser")]
    public class FileBrowser : TagHelper
    {
        public string Placeholder { get; set; }
        public string OnClickAction { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format(@"<div class='form-group'>
                                    <button onclick='{0}' class='fileBrowser fas fa-paperclip'>&nbsp; انتخاب فایل</button>
                                    <input id='txt-search' placeholder='{1}' type='text' style='height: 43px;width: 416px;margin-right: -9px;'>
                            </div>", OnClickAction, Placeholder);

            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("tab-Name")]
    public class TabName : TagHelper
    {
        public bool isActive { get; set; } = false;
        public string targetTab { get; set; }
        public string TabTitle { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format(@"<li class='{0}'><a data-toggle='tab' href='{1}'>{2}</a></li>", isActive ? "active" : "", "#" + targetTab, TabTitle);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("tab-Data")]
    public class TabData : TagHelper
    {
        public bool isActive { get; set; } = false;
        public string Id { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var divDetails = new TagBuilder("div");
            divDetails.Attributes.Add("id", Id);
            divDetails.Attributes.Add("class", isActive ? "tab-pane fade in active" : "tab-pane fade");
            //divDetails.InnerHtml.AppendHtml(tabStripContext.TabDetails);
            output.Content.AppendHtml(divDetails);

            //string html = string.Empty;
            //html += string.Format(@"<div id='{0}' class='{1}'></div>", Id, isActive ? "tab-pane fade in active" : "tab-pane fade");

            //output.TagMode = TagMode.StartTagAndEndTag;
            //output.TagName = null;
            //var sb = new StringBuilder();
            //sb.AppendFormat(html);
            //output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("collapse-header")]
    public class CollapseHeader : TagHelper
    {
        public string Title { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format(@"<div class='header'><span>{0}</span>", Title);
            html += "<i class='fa fa-sort-desc' aria-hidden='true'></i></div>";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("item-collapse")]
    public class CollapseItemDetails : TagHelper
    {
        public string IconClass { get; set; }
        public DataTable ModelListTable { get; set; }
        public List<string> HeaderTitle { get; set; }
        public string HeaderName { get; set; }
        public string OnClickEvent { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            int counter = 1;
            string html = string.Empty;

            html += "<div class='containerCollapse'>";
            html += string.Format("<div class='header'><span style='margin-left:5px;'>{0}</span><i class='fa fa-sort-desc' aria-hidden='true'></i></div>", HeaderName);
            html += "<div class='content'>";

            for (int i = 0; i < ModelListTable.Rows.Count; i++)
            {
                if (counter % 5 == 0 || counter == 1)
                    html += "<div class='row' style='margin-top:10px;'>";

                html += string.Format("<div class='col-lg-4 collapsItems' onclick='{0}'><div><i class='{1}' aria-hidden='true' style='font-size:34px;text-align:center;'></i></div>", OnClickEvent, IconClass);

                for (int j = 0; j < HeaderTitle.Count; j++)
                {
                    if (j == 0)
                        html += string.Format("<div><span class='collapseId' style='display:none;'>{0}</span></div>", ModelListTable.Rows[i][j]);
                    else
                    {
                        html += string.Format(@"<div><span style='font-size:1.2em;'> {0} </span>", HeaderTitle[j] + " : ");
                        html += string.Format(@"<span>{0}</span></div>", ModelListTable.Rows[i][j]); // FIRST COLUMN ALWAYS MUST BE ID 
                    }
                }
                html += "</div>";

                if (counter % 5 == 0 || counter == ModelListTable.Rows.Count)
                    html += "</div>";

                counter += 1;
            }

            html += "</div></div></div>";
            output.TagMode = TagMode.SelfClosing;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("li-Item")]
    public class Li_ItemTagHelper : TagHelper
    {
        public string Id { get; set; }
        public bool? IsSpecialFields { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<li style='background:transparent;' class='{0}'>", IsSpecialFields == true ? "list-group-item d-flex justify-content-between border-0 pb-0 specialTd" : "list-group-item d-flex justify-content-between border-0 pb-0");
            html += string.Format("<h6 class='invoice-subtotal-value mb-0' id='{0}'></h6> </li>", Id);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("li-ItemVerticalMenu")]
    public class Li_ItemVerticalMenuTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string MenuTitle { get; set; }
        public string Link { get; set; }
        public string IconImage { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<li class='nav-item'><a class='nav-link d-flex align-items-center' id='{0}' data-toggle='pill' href='{1}' aria-expanded='false'>", Id, "#" + Link);
            html += string.Format("<i class='{0}'></i><span> {1} </span></a></li>", IconImage, MenuTitle);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("ImageSelector")]
    public class ImageSelectorTagHelper : TagHelper
    {
        public string ImgSrc { get; set; }
        public string ImageAlt { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += "<div class='media'>";
            html += "<a href='javascript:%20void(0);'>";
            html += string.Format("<img src='{0}' class='rounded mr-75' alt='{1}' height='{2}' width='{3}'></a>", ImgSrc, ImageAlt, Height, Width);
            html += "<div class='media-body mt-25'>";
            html += "<div class='col-12 px-0 d-flex flex-sm-row flex-column justify-content-start'>";
            html += "<label for='select-files' class='btn btn-sm btn-light-primary ml-50 mb-50 mb-sm-0'>";
            html += "<span>ارسال تصویر جدید</span>";
            html += "<input id='select-files' type='file' hidden>";
            html += "<button class='btn btn-sm btn-light-secondary ml-50'>بازنشانی</button> </div>";
            html += " <p class='text-muted ml-1 mt-50'><small>فایل های مجاز: JPG، PNG و GIF. حداکثر اندازه مجاز: 800KB</small></p>";
            html += "</div></div> <hr>";


            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    [HtmlTargetElement("tab-Item-li")]
    public class ButtonTagHelper : TagHelper
    {
        public bool IsActive { get; set; }
        public string Href{ get; set; }
        public string ClassName { get; set; }
        public string Title { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += string.Format("<li class='nav-item current'><a class='{0}' id='account-tab' role='tab' aria-selected='true' aria-controls='{1}' href='{2}' data-toggle='tab'>",IsActive ? "nav-link d-flex align-items-center active" : "nav-link d-flex align-items-center", Href , "#" + Href);
            html += string.Format("<i class='{0}'></i><span class='d-none d-sm-block'>{1}</span></a></li >" , ClassName , Title) ;

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = null;
            var sb = new StringBuilder();
            sb.AppendFormat(html);
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }



}

