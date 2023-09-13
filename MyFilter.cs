using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Filter
{
    public class MyFilter:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //}
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnResultExecuted(filterContext);
        //}
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //}
    }
}