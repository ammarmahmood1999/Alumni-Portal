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
    public class job_postingController : ApiController
    {
        private IPT2020Entities4 db = new IPT2020Entities4();

        // GET: api/job_posting
        public IQueryable<job_posting> Getjob_posting()
        {
            return db.job_posting;
        }

        // GET: api/job_posting/5
        [ResponseType(typeof(job_posting))]
        public IHttpActionResult Getjob_posting(int id)
        {
            job_posting job_posting = db.job_posting.Find(id);
            if (job_posting == null)
            {
                return NotFound();
            }

            return Ok(job_posting);
        }

        // PUT: api/job_posting/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putjob_posting(job_posting job_posting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            db.Entry(job_posting).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!job_postingExists(job_posting.id))
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

        // POST: api/job_posting
        [ResponseType(typeof(job_posting))]
        public IHttpActionResult Postjob_posting(job_posting job_posting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.job_posting.Add(job_posting);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job_posting.id }, job_posting);
        }

        // DELETE: api/job_posting/5
        [ResponseType(typeof(job_posting))]
        public IHttpActionResult Deletejob_posting(int id)
        {
            job_posting job_posting = db.job_posting.Find(id);
            if (job_posting == null)
            {
                return NotFound();
            }

            db.job_posting.Remove(job_posting);
            db.SaveChanges();

            return Ok(job_posting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool job_postingExists(int id)
        {
            return db.job_posting.Count(e => e.id == id) > 0;
        }
    }
}