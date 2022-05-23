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
    public class MascotasController : ApiController
    {
        private ApiEntities db = new ApiEntities();

        // GET: api/Mascotas
        public IQueryable<Mascota> GetMascota()
        {
            return db.Mascota;
        }



        [Route("api/Mascotas/GetMascotaByRefugio/{idRefugio}")]
        // GET: api/Mascotas/5
        [ResponseType(typeof(List<Mascota>))]
        public IHttpActionResult GetMascotaByRefugio(int idRefugio)
        {
            List<Mascota> mascotas = db.Mascota.
                Where(m=>m.IdRefugio==idRefugio).ToList();
            if (mascotas == null)
            {
                return NotFound();
            }

            return Ok(mascotas);
        }

        // PUT: api/Mascotas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMascota(int id, Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mascota.IdMascota)
            {
                return BadRequest();
            }

            db.Entry(mascota).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MascotaExists(id))
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
        [ResponseType(typeof(Mascota))]
        public IHttpActionResult PostMascota(Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mascota.Add(mascota);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mascota.IdMascota }, mascota);
        }

        // DELETE: api/Mascotas/5
        [ResponseType(typeof(Mascota))]
        public IHttpActionResult DeleteMascota(int id)
        {
            Mascota mascota = db.Mascota.Find(id);
            if (mascota == null)
            {
                return NotFound();
            }

            db.Mascota.Remove(mascota);
            db.SaveChanges();

            return Ok(mascota);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MascotaExists(int id)
        {
            return db.Mascota.Count(e => e.IdMascota == id) > 0;
        }
    }
}