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
    public class alumniController : ApiController
    {
        private IPT2020Entities db = new IPT2020Entities();

        // GET: api/alumni
        public IQueryable<alumnus> Getalumni()
        {
            return db.alumni;
        }

        // GET: api/alumni/5
        [ResponseType(typeof(alumnus))]
        public IHttpActionResult Getalumnus(int id)
        {
            alumnus alumnus = db.alumni.Find(id);
            if (alumnus == null)
            {
                return NotFound();
            }

            return Ok(alumnus);
        }

        // PUT: api/alumni/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putalumnus(int id, alumnus alumnus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumnus.id)
            {
                return BadRequest();
            }

            db.Entry(alumnus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!alumnusExists(id))
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

        // POST: api/alumni
        [ResponseType(typeof(alumnus))]
        public IHttpActionResult Postalumnus(alumnus alumnus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.alumni.Add(alumnus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alumnus.id }, alumnus);
        }

        // DELETE: api/alumni/5
        [ResponseType(typeof(alumnus))]
        public IHttpActionResult Deletealumnus(int id)
        {
            alumnus alumnus = db.alumni.Find(id);
            if (alumnus == null)
            {
                return NotFound();
            }

            db.alumni.Remove(alumnus);
            db.SaveChanges();

            return Ok(alumnus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool alumnusExists(int id)
        {
            return db.alumni.Count(e => e.id == id) > 0;
        }
    }
}