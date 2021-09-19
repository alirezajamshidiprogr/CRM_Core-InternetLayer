using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UI_Presentation.Models
{
    public class Authorization : ActionFilterAttribute
    {
        public Authorization()
        {
            int a = 4; 
        }
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext) { base.OnActionExecuted(filterContext); }
        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext) { base.OnResultExecuting(filterContext); }
        public override void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext) { base.OnResultExecuted(filterContext); }
    }
}
