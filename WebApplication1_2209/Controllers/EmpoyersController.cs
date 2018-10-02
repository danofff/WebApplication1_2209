using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using PagedList;

namespace WebApplication1_2209.Controllers
{
    public class EmpoyersController : Controller
    {
        private static users users;
        // GET: Empoyers
        public ActionResult Index()
        {
            using(WebClient wc=new WebClient())
            {
                string result=
                wc.DownloadString("https://randomuser.me/api?results=100");

                users = Newtonsoft.Json.
                    JsonConvert.DeserializeObject<users>(result);


            }
            return View();
        }

        [HttpGet]
      public ActionResult GetInfo(string uuid)
        {
          var result= users.results.FirstOrDefault(f => f.login.uuid == uuid);
            return PartialView("_Info",result);
        }

        public ActionResult GetEmployers(int? page, int countEmployer)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue
                        ? Convert.ToInt32(page)
                        : 1;

            IPagedList<results> result = users.results
                           .ToPagedList(pageIndex, pageSize);

            return PartialView("_Employers", result);
        }

    }
    public class users
    {
        public List<results> results { get; set; }
    }

    public class results
    {
        public string gender { get; set; }
        public name name { get; set; }
        public picture picture { get; set; }
        public login login { get; set; }
        public dob dob { get; set; }
        public location location { get; set; }
        public string cell { get; set; }
    }

    public class location
    {
        public string city { get; set; }
        public string street { get; set; }
    }

    


    public class name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class dob
    {
        public DateTime date { get; set; }
        public int age { get; set; }
    }
    public class picture
    {
        public string thumbnail { get; set; }
        public string medium { get; set; }
    }
    public class login
    {
        public string uuid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}