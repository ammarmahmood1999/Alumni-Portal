using FAST_Alumni_2020_Web_Portal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FAST_Alumni_2020_Web_Portal.Controllers
{
    public class AuthController : Controller
    {
        private string Link = ConfigurationManager.AppSettings["LinkToAccessAPI"];
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
    
        public ActionResult Login(AlumniLogin objUser)
        {
            if (ModelState.IsValid)
            {

                AlumniLogin obj1 = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Link);
                    //HTTP GET
                    string username = objUser.username;
                    var responseTask = client.GetAsync("AlumniLogins/" + username);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();//<IList<JobPosting>>();
                        readTask.Wait();
                        AlumniLogin obj = JsonConvert.DeserializeObject<AlumniLogin>(readTask.Result);
                        obj1 = obj;
                    }

                    else //web api sent error response 
                    {
                        //log response status here..
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                if (obj1 != null)
                {
                    //Session["UserID"] = obj1.username.ToString();
                    //Session["UserName"] = obj1.username.ToString();
                    if (obj1.password.Equals(objUser.password))
                    {
                        Session["UserID"] = obj1.AlumniId;
                        Session["UserName"] = obj1.username.ToString();
                        return RedirectToAction("Index", "JobsPosting");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username or Password Incorrect");
                    }
                }
                
            }
            return View(objUser);
        }
        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
