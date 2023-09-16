using DAL;
using System;
using System.Web.Mvc;

namespace News.Filter
{
    public class ExceptionFilter :FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var ctx = new NewsEntities();
            ErrorLog lg = new ErrorLog();
            
            lg.Error = filterContext.Exception.ToString().Substring(150);
            lg.RawId = filterContext.RequestContext.HttpContext.Request.RawUrl;
            lg.CreateDate = DateTime.Now;

            ctx.ErrorLog.Add(lg);
            ctx.SaveChanges();
          
            
            
            filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Error");
            filterContext.RequestContext.HttpContext.Response.End();
        }
    }
}