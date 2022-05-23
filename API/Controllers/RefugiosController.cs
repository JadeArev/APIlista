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
    public class RefugiosController : ApiController
    {
        private ApiEntities db = new ApiEntities();

        [Route("api/refugios/getrefugio/{id}")]
        // GET: api/Refugios/5
        [ResponseType(typeof(Refugio))]
        public IHttpActionResult GetRefugio(int id)
        {
            Refugio refugio = db.Refugio.Find(id);
            if (refugio == null)
            {
                return NotFound();
            }

            return Ok(refugio);
        }

        // PUT: api/Refugios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRefugio(int id, Refugio refugio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != refugio.IdRefugio)
            {
                return BadRequest();
            }

            db.Entry(refugio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefugioExists(id))
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
        [ResponseType(typeof(Refugio))]
        public IHttpActionResult PostRefugio(Refugio refugio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Refugio.Add(refugio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = refugio.IdRefugio }, refugio);
        }

        // DELETE: api/Refugios/5
        [ResponseType(typeof(Refugio))]
        public IHttpActionResult DeleteRefugio(int id)
        {
            Refugio refugio = db.Refugio.Find(id);
            if (refugio == null)
            {
                return NotFound();
            }

            db.Refugio.Remove(refugio);
            db.SaveChanges();

            return Ok(refugio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RefugioExists(int id)
        {
            return db.Refugio.Count(e => e.IdRefugio == id) > 0;
        }
    }
}