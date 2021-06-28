using System;
using System.Web.Mvc;

namespace CRM.Utility
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,
     AllowMultiple = true, Inherited = true)]
    public class RangeExceptionOnAttribute : HandleErrorAttribute
    {
      
        public override void OnException(ExceptionContext filterContext)
        {

            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult { ViewName = "~/Views/Login/Index.cshtml" };
            
        }
    }
}