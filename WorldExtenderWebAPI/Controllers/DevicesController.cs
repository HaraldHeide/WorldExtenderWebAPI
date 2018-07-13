using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WorldExtenderDataAccess;

namespace WorldExtenderWebAPI.Controllers
{
    public class DevicesController : ApiController
    {
        private WorldExtenderEntities db = new WorldExtenderEntities();

        // GET: api/IoT_Device
        public IQueryable<IoT_Device> GetIoT_Device()
        {
            return db.IoT_Device;
        }

        // GET: api/IoT_Device/5
        [ResponseType(typeof(IoT_Device))]
        public async Task<IHttpActionResult> GetIoT_Device(int id)
        {
            IoT_Device ioT_Device = await db.IoT_Device.FindAsync(id);
            if (ioT_Device == null)
            {
                return NotFound();
            }

            return Ok(ioT_Device);
        }

        // PUT: api/IoT_Device/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIoT_Device(int id, IoT_Device ioT_Device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ioT_Device.Id)
            {
                return BadRequest();
            }

            db.Entry(ioT_Device).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IoT_DeviceExists(id))
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

        // POST: api/IoT_Device
        //[ResponseType(typeof(IoT_Device))]
        //public async Task<IHttpActionResult> PostIoT_Device(IoT_Device ioT_Device)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.IoT_Device.Add(ioT_Device);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = ioT_Device.Id }, ioT_Device);
        //}
        [HttpPost]
        public  HttpResponseMessage Post([FromBody] IoT_Device ioT_Device)
        {
            try
            {
                using (WorldExtenderEntities entities = new WorldExtenderEntities())
                {
                    entities.IoT_Device.Add(ioT_Device);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, ioT_Device);
                    message.Headers.Location = new Uri(Request.RequestUri + ioT_Device.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/IoT_Device/5
        [ResponseType(typeof(IoT_Device))]
        public async Task<IHttpActionResult> DeleteIoT_Device(int id)
        {
            IoT_Device ioT_Device = await db.IoT_Device.FindAsync(id);
            if (ioT_Device == null)
            {
                return NotFound();
            }

            db.IoT_Device.Remove(ioT_Device);
            await db.SaveChangesAsync();

            return Ok(ioT_Device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IoT_DeviceExists(int id)
        {
            return db.IoT_Device.Count(e => e.Id == id) > 0;
        }
    }
}