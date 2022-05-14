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
    public class RefugiosController : ApiController
    {
        private ApiEntities db = new ApiEntities();

        // GET: api/Refugios
        public IQueryable<Refugios> GetRefugios()
        {
            return db.Refugios;
        }

        // GET: api/Refugios/5
        [ResponseType(typeof(Refugios))]
        public IHttpActionResult GetRefugios(int id)
        {
            Refugios refugios = db.Refugios.Find(id);
            if (refugios == null)
            {
                return NotFound();
            }

            return Ok(refugios);
        }

        // PUT: api/Refugios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRefugios(int id, Refugios refugios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != refugios.IdRefugios)
            {
                return BadRequest();
            }

            db.Entry(refugios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefugiosExists(id))
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

        // POST: api/Refugios
        [ResponseType(typeof(Refugios))]
        public IHttpActionResult PostRefugios(Refugios refugios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Refugios.Add(refugios);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = refugios.IdRefugios }, refugios);
        }

        // DELETE: api/Refugios/5
        [ResponseType(typeof(Refugios))]
        public IHttpActionResult DeleteRefugios(int id)
        {
            Refugios refugios = db.Refugios.Find(id);
            if (refugios == null)
            {
                return NotFound();
            }

            db.Refugios.Remove(refugios);
            db.SaveChanges();

            return Ok(refugios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RefugiosExists(int id)
        {
            return db.Refugios.Count(e => e.IdRefugios == id) > 0;
        }
    }
}