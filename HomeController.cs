using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using FirstMVC.Models;

namespace FirstMVC.Controllers
{
    [MyExceptionFilter]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if(Session["User"] == null)
            {
                return View("Login",new LoginViewModel());
            }
            var crx = new CareerEntities();
            var users = (Users)Session["User"];
            Candidate c = crx.Candidate.FirstOrDefault(x=>x.UserId==users.Id);
           
            List<CandidateApplicant> basvurularim = crx.CandidateApplicant.Where(y => y.CandidateId == c.Id).ToList();
            List<Applicant> aplList= crx.Applicant.ToList();

            //işler
            foreach (var item in crx.Applicant.ToList())
            {
                foreach (var item2 in basvurularim)
                {
                    if (item.JobId == item2.Applicant.JobId)
                    {
                        aplList.Remove(item);
                    }
                }
            }


            ViewBag.BasvuruJobs = basvurularim;
            ViewBag.BasvurulmayanJobs = aplList;
            return View(c);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Your error page.";

            return View();
        }
    }
}