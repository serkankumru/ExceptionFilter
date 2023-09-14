using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Filter
{
    public class NewsStatusFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int id = Convert.ToInt32(filterContext.ActionParameters["id"]);
            
			
        }
    }
}