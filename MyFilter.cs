using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstMVC.Extensions;

namespace FirstMVC.Models
{
    public class MyExceptionFilter :FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var ctx = new CareerEntities();
            ErrorLog lg = new ErrorLog();
            if (filterContext.RequestContext.HttpContext.Session["User"] != null)
                lg.UserId = ((Users)filterContext.RequestContext.HttpContext.Session["User"]).Id;
            lg.error = filterContext.Exception.ToString().ControlledSub(1000, "");
            lg.rawId = filterContext.RequestContext.HttpContext.Request.RawUrl;
            lg.createDate = DateTime.Now;

            ctx.ErrorLog.Add(lg);
            ctx.SaveChanges();
          
            
            
            filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Error");
            filterContext.RequestContext.HttpContext.Response.End();
        }
    }
}