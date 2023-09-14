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
        public string test(int id,string name)
        {
            return "<h1>idsi "+id+" name "+name+"</h1>";
        }


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

        
        public ActionResult About()
        {
            List<FundSegmen> fundSegmensList = new List<FundSegmen>();
            fundSegmensList.Add(new FundSegmen() { Id = 1, MaxValue = 100000, MinValue = 0 });
            fundSegmensList.Add(new FundSegmen() { Id = 1, MaxValue = 500000, MinValue = 100000 });
            fundSegmensList.Add(new FundSegmen() { Id = 1, MaxValue = 1000000, MinValue = 500000 });
            fundSegmensList.Add(new FundSegmen() { Id = 1, MaxValue = 5000000, MinValue = 1000000 });

            //int a = 0;
            //int b = 5;
            //int x = b / a;
            return View(fundSegmensList);

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


        public ActionResult Basvuru(int candidateId, int applicantId,bool islem)
        {
            var crx = new CareerEntities();
            if (islem)
            {
                CandidateApplicant ca = new CandidateApplicant();
                ca.ApplicantId = applicantId;
                ca.CandidateId = candidateId;
                crx.CandidateApplicant.Add(ca);
            }
            else
            {
                CandidateApplicant ca = crx.CandidateApplicant.Where(x => x.CandidateId == candidateId && x.ApplicantId == applicantId).FirstOrDefault();
                crx.CandidateApplicant.Remove(ca);

            }
            crx.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var ctx = new  CareerEntities();
            var usr = ctx.Users.FirstOrDefault(c=>c.UserName==model.UserName&&c.Password==model.Password);
            if (usr != null)
            {
                Session["User"] = usr;
                
                var crx = new CareerEntities();
                Candidate c = ctx.Candidate.FirstOrDefault(x => x.UserId == usr.Id);
                List<CandidateApplicant> basvurularim = crx.CandidateApplicant.Where(y => y.CandidateId == c.Id).ToList();
                List<Applicant> aplList = crx.Applicant.ToList();

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
                return View("Index",c);
            }
            else
            {
                model.Hata = "Hatalı giriş. :(";
            }
            return View("Login", model);
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return View("Login", new LoginViewModel());
        }
    }
}