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
    public class JobsPostingController : Controller
    {
        private string Link = ConfigurationManager.AppSettings["LinkToAccessAPI"];
        // GET: JobsPosting
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login","Auth");
            IEnumerable<JobPosting> students = null;
            List<JobPosting> model = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);
                //HTTP GET
                var responseTask = client.GetAsync("job_posting");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();//<IList<JobPosting>>();
                    readTask.Wait();
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JobPosting>>(readTask.Result);

                    students = model;
                }

                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<JobPosting>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            Organization obj_1;
            List<JobPostFinal> postFinals = new List<JobPostFinal>();
            foreach (var jobPosting in students)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Link);
                    //HTTP GET
                    var responseTask = client.GetAsync("organizations/" + jobPosting.organizationId);
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

                        students = Enumerable.Empty<JobPosting>();

                        obj_1 = new Organization();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                }

                JobPostFinal jobPost = new JobPostFinal(jobPosting.id,jobPosting.title,jobPosting.description,jobPosting.skils,obj_1.name,obj_1.id,obj_1.location);
                postFinals.Add(jobPost);
            }


            return View(postFinals);
        }

        // GET: JobsPosting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobsPosting/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Auth");
            IEnumerable<Organization> organizations = null;
            using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Link);
                    //HTTP GET
                    var responseTask = client.GetAsync("organizations" );
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync(); //<IList<JobPosting>>();
                        readTask.Wait();
                        dynamic obj = JsonConvert.DeserializeObject<List<Organization>>(readTask.Result);
                        organizations = obj;
                    }

                    else //web api sent error response 
                    {
                    //log response status here..
                    organizations = Enumerable.Empty<Organization>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                }

            SelectListItem selListItem = new SelectListItem() { Value = "null", Text = "Select One" };

            //Create a list of select list items - this will be returned as your select list
            List<SelectListItem> newList = new List<SelectListItem>();

            //Add select list item to list of selectlistitems
            newList.Add(selListItem);

            //Return the list of selectlistitems as a selectlist
            foreach (var VARIABLE in organizations)
            {
                newList.Add(new SelectListItem() { Value = VARIABLE.id.ToString(), Text = VARIABLE.name });
            }

            ViewBag.Data = new SelectList(newList, "Value", "Text", null);
            return View();
        }

        // POST: JobsPosting/Create
        [HttpPost]
        public ActionResult Create(JobPosting collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link +"job");
                collection.alumniId = ((int)Session["UserID"]);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<JobPosting>("job_posting", collection);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View();
        }

        // GET: JobsPosting/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Auth");
            IEnumerable<JobPosting> students = null;
            List<JobPosting> model = null;
            JobPosting obj1;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);
                //HTTP GET
                var responseTask = client.GetAsync("job_posting/"+id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();//<IList<JobPosting>>();
                    readTask.Wait();
                    JobPosting obj = JsonConvert.DeserializeObject<JobPosting>(readTask.Result);
                    obj1 = obj;
                    students = model;
                }

                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<JobPosting>();
                    
                    obj1 = new JobPosting();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(obj1);
        
        }

        // POST: JobsPosting/Edit/5
        [HttpPost]
        public ActionResult Edit(JobPosting collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link+ "job_posting");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<JobPosting>("job_posting", collection);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(collection);
        }

        // GET: JobsPosting/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("job_posting/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        // POST: JobsPosting/Delete/5
        /*[HttpPost]
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
        }*/
    }
}
