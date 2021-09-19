using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI_Presentation.Models
{
    public class searchHeaderContext
    {
        public IHtmlContent SearchButton { get; set; }
        public IHtmlContent SearchTextBox { get; set; }
    }
}
