using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FASTAlumniPortal_Web.Models;

namespace FASTAlumniPortal_Web.Controllers
{
    public class jobsController : ApiController
    {
        private IPT2020Entities7 db = new IPT2020Entities7();

        // GET: api/jobs
        public IQueryable<job> Getjobs()
        {
            return db.jobs;
        }

        // GET: api/jobs/5
        [ResponseType(typeof(job))]
        public IHttpActionResult Getjob(int id)
        {
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT: api/jobs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putjob(int id, job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.id)
            {
                return BadRequest();
            }

            db.Entry(job).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!jobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/jobs
        [ResponseType(typeof(job))]
        public IHttpActionResult Postjob(job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.jobs.Add(job);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job.id }, job);
        }

        // DELETE: api/jobs/5
        [ResponseType(typeof(job))]
        public IHttpActionResult Deletejob(int id)
        {
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            db.jobs.Remove(job);
            db.SaveChanges();

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool jobExists(int id)
        {
            return db.jobs.Count(e => e.id == id) > 0;
        }
    }
}