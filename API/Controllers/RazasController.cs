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
using Api.Models;

namespace Api.Controllers
{
    public class RazasController : ApiController
    {
        private ApiEntities db = new ApiEntities();

        // GET: api/Razas
        public IQueryable<Raza> GetRaza()
        {
            return db.Raza;
        }

        // GET: api/Razas/5
        [ResponseType(typeof(Raza))]
        public IHttpActionResult GetRaza(int id)
        {
            Raza raza = db.Raza.Find(id);
            if (raza == null)
            {
                return NotFound();
            }

            return Ok(raza);
        }

        // PUT: api/Razas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRaza(int id, Raza raza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != raza.IdRaza)
            {
                return BadRequest();
            }

            db.Entry(raza).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RazaExists(id))
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

        // POST: api/Razas
        [ResponseType(typeof(Raza))]
        public IHttpActionResult PostRaza(Raza raza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Raza.Add(raza);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = raza.IdRaza }, raza);
        }

        // DELETE: api/Razas/5
        [ResponseType(typeof(Raza))]
        public IHttpActionResult DeleteRaza(int id)
        {
            Raza raza = db.Raza.Find(id);
            if (raza == null)
            {
                return NotFound();
            }

            db.Raza.Remove(raza);
            db.SaveChanges();

            return Ok(raza);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RazaExists(int id)
        {
            return db.Raza.Count(e => e.IdRaza == id) > 0;
        }
    }
}