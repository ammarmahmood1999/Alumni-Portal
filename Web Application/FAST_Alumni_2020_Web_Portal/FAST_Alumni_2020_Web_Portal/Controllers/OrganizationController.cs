using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using FAST_Alumni_2020_Web_Portal.Models;
using Newtonsoft.Json;

namespace FAST_Alumni_2020_Web_Portal.Controllers
{
    public class OrganizationController : Controller
    {
        private string Link = ConfigurationManager.AppSettings["LinkToAccessAPI"];
        // GET: Organization
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Auth");
            IEnumerable<Organization> students = null;
            List<Organization> model = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);
                //HTTP GET
                var responseTask = client.GetAsync("organizations");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();//<IList<JobPosting>>();
                    readTask.Wait();
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Organization>>(readTask.Result);

                    students = model;
                }

                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<Organization>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }


            return View(students);
        }

        // GET: Organization/Details/5
        public ActionResult Details(int id)
        {
            Organization obj_1;
            using (var client = new HttpClient())
            {
               
                client.BaseAddress = new Uri(Link);
                //HTTP GET
                var responseTask = client.GetAsync("organizations/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync(); //<IList<JobPosting>>();
                    readTask.Wait();
                    Organization obj = JsonConvert.DeserializeObject<Organization>(readTask.Result);
                    obj_1 = obj;
                }

                else //web api sent error response 
                {
                    //log response status here..

                    //students = Enumerable.Empty<JobPosting>();

                    obj_1 = new Organization();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }


            return View(obj_1);
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Auth");
            return View();
        }

        // POST: Organization/Create
        [HttpPost]
        public ActionResult Create(Organization collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Link + "organizations");
                    

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Organization>("organizations", collection);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return RedirectToAction("");
            }
            catch
            {
                return View();
            }
        }

        // GET: Organization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Organization/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Organization/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Organization/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
