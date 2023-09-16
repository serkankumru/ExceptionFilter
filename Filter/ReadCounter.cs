using DAL;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Haberler.Filter
{
    public class ReadCounter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters["id"] != null)
            {
                int id = Extensions.Extensions.GetNewsTitleId(filterContext.ActionParameters["id"].ToString());
                if (id != 0)
                {
                    var newsRepository = new NewsRepository();
                    newsRepository.ReadCount(id);
                }
            }               
        }
    }
}