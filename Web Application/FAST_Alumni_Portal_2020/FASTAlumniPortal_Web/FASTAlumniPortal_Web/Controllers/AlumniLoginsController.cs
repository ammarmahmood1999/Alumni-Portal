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
    public class AlumniLoginsController : ApiController
    {
        private IPT2020Entities8 db = new IPT2020Entities8();

        // GET: api/AlumniLogins
        public IQueryable<AlumniLogin> GetAlumniLogins()
        {
            return db.AlumniLogins;
        }

        // GET: api/AlumniLogins/5
        [ResponseType(typeof(AlumniLogin))]
        public IHttpActionResult GetAlumniLogin(string id)
        {
            string username = id;
            var obj = db.AlumniLogins.Where(a => a.username.Equals(username.ToString())).FirstOrDefault();
            if (obj != null)
            {
                return Ok(obj);
            }
            return NotFound();
        }

        // PUT: api/AlumniLogins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlumniLogin(int id, AlumniLogin alumniLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumniLogin.id)
            {
                return BadRequest();
            }

            db.Entry(alumniLogin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumniLoginExists(id))
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

        // POST: api/AlumniLogins
        [ResponseType(typeof(AlumniLogin))]
        public IHttpActionResult PostAlumniLogin(AlumniLogin alumniLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AlumniLogins.Add(alumniLogin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alumniLogin.id }, alumniLogin);
        }

        // DELETE: api/AlumniLogins/5
        [ResponseType(typeof(AlumniLogin))]
        public IHttpActionResult DeleteAlumniLogin(int id)
        {
            AlumniLogin alumniLogin = db.AlumniLogins.Find(id);
            if (alumniLogin == null)
            {
                return NotFound();
            }

            db.AlumniLogins.Remove(alumniLogin);
            db.SaveChanges();

            return Ok(alumniLogin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlumniLoginExists(int id)
        {
            return db.AlumniLogins.Count(e => e.id == id) > 0;
        }
    }
}