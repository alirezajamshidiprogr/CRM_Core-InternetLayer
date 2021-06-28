using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UI_Presentation.Models
{
    public class Log : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //og("OnResultExecuting ", filterContext.RouteData);
        }

        private void Logg(string methodName, RouteData routeData)
        {
            var controllerName = routeData.RouteValues["controller"];
            var actionName = routeData.RouteValues["action"];
            var message = String.Format("{0}- controller:{1} action:{2}", methodName,
                                                                        controllerName,
                                                                        actionName);
            Debug.WriteLine(message);
        }
    }
}
