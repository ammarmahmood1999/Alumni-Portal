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
    public class hiredalumniController : ApiController
    {
        private IPT2020Entities3 db = new IPT2020Entities3();

        // GET: api/hiredalumni
        public IQueryable<hired_alumni> Gethired_alumni()
        {
            return db.hired_alumni;
        }

        // GET: api/hiredalumni/5
        [ResponseType(typeof(hired_alumni))]
        public IHttpActionResult Gethired_alumni(int id)
        {
            hired_alumni hired_alumni = db.hired_alumni.Find(id);
            if (hired_alumni == null)
            {
                return NotFound();
            }

            return Ok(hired_alumni);
        }

        // PUT: api/hiredalumni/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puthired_alumni(int id, hired_alumni hired_alumni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hired_alumni.alumni_id)
            {
                return BadRequest();
            }

            db.Entry(hired_alumni).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!hired_alumniExists(id))
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

        // POST: api/hiredalumni
        [ResponseType(typeof(hired_alumni))]
        public IHttpActionResult Posthired_alumni(hired_alumni hired_alumni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.hired_alumni.Add(hired_alumni);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (hired_alumniExists(hired_alumni.alumni_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hired_alumni.alumni_id }, hired_alumni);
        }

        // DELETE: api/hiredalumni/5
        [ResponseType(typeof(hired_alumni))]
        public IHttpActionResult Deletehired_alumni(int id)
        {
            hired_alumni hired_alumni = db.hired_alumni.Find(id);
            if (hired_alumni == null)
            {
                return NotFound();
            }

            db.hired_alumni.Remove(hired_alumni);
            db.SaveChanges();

            return Ok(hired_alumni);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool hired_alumniExists(int id)
        {
            return db.hired_alumni.Count(e => e.alumni_id == id) > 0;
        }
    }
}