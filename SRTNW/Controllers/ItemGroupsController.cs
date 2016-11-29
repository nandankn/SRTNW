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
using SRTNW.Models;

namespace SRTNW.Controllers
{
    public class ItemGroupsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ItemGroups
        public IQueryable<ItemGroup> GetItemGroups()
        {
            return db.ItemGroups.Include(a => a.Items);
        }

        // GET: api/ItemGroups/5
        [ResponseType(typeof(ItemGroup))]
        public async Task<IHttpActionResult> GetItemGroup(string id)
        {
            ItemGroup itemGroup = await db.ItemGroups.FindAsync(id);
            if (itemGroup == null)
            {
                return NotFound();
            }

            return Ok(itemGroup);
        }

        // PUT: api/ItemGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItemGroup(string id, ItemGroup itemGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemGroup.Id)
            {
                return BadRequest();
            }

            db.Entry(itemGroup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemGroupExists(id))
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

        // POST: api/ItemGroups
        [ResponseType(typeof(ItemGroup))]
        public async Task<IHttpActionResult> PostItemGroup(ItemGroup itemGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemGroups.Add(itemGroup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemGroupExists(itemGroup.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = itemGroup.Id }, itemGroup);
        }

        // DELETE: api/ItemGroups/5
        [ResponseType(typeof(ItemGroup))]
        public async Task<IHttpActionResult> DeleteItemGroup(string id)
        {
            ItemGroup itemGroup = await db.ItemGroups.FindAsync(id);
            if (itemGroup == null)
            {
                return NotFound();
            }

            db.ItemGroups.Remove(itemGroup);
            await db.SaveChangesAsync();

            return Ok(itemGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemGroupExists(string id)
        {
            return db.ItemGroups.Count(e => e.Id == id) > 0;
        }
    }
}