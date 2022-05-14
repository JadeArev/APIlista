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
using API.Models;

namespace API.Controllers
{
    public class MascotasController : ApiController
    {
        private ApiEntities db = new ApiEntities();

        // GET: api/Mascotas
        public IQueryable<Mascotas> GetMascotas()
        {
            return db.Mascotas;
        }

        // GET: api/Mascotas/5
        [ResponseType(typeof(Mascotas))]
        public IHttpActionResult GetMascotas(int id)
        {
            Mascotas mascotas = db.Mascotas.Find(id);
            if (mascotas == null)
            {
                return NotFound();
            }

            return Ok(mascotas);
        }

        // PUT: api/Mascotas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMascotas(int id, Mascotas mascotas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mascotas.IdMascotas)
            {
                return BadRequest();
            }

            db.Entry(mascotas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MascotasExists(id))
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

        // POST: api/Mascotas
        [ResponseType(typeof(Mascotas))]
        public IHttpActionResult PostMascotas(Mascotas mascotas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mascotas.Add(mascotas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mascotas.IdMascotas }, mascotas);
        }

        // DELETE: api/Mascotas/5
        [ResponseType(typeof(Mascotas))]
        public IHttpActionResult DeleteMascotas(int id)
        {
            Mascotas mascotas = db.Mascotas.Find(id);
            if (mascotas == null)
            {
                return NotFound();
            }

            db.Mascotas.Remove(mascotas);
            db.SaveChanges();

            return Ok(mascotas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MascotasExists(int id)
        {
            return db.Mascotas.Count(e => e.IdMascotas == id) > 0;
        }
    }
}