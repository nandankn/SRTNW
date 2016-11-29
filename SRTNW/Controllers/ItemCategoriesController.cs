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
    public class ItemCategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ItemCategories
        public IQueryable<ItemCategory> GetItemCategories()
        {
            return db.ItemCategories;
        }

        // GET: api/ItemCategories/5
        [ResponseType(typeof(ItemCategory))]
        public async Task<IHttpActionResult> GetItemCategory(string id)
        {
            ItemCategory itemCategory = await db.ItemCategories.FindAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            return Ok(itemCategory);
        }

        // PUT: api/ItemCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItemCategory(string id, ItemCategory itemCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(itemCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCategoryExists(id))
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

        // POST: api/ItemCategories
        [ResponseType(typeof(ItemCategory))]
        public async Task<IHttpActionResult> PostItemCategory(ItemCategory itemCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemCategories.Add(itemCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemCategoryExists(itemCategory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = itemCategory.Id }, itemCategory);
        }

        // DELETE: api/ItemCategories/5
        [ResponseType(typeof(ItemCategory))]
        public async Task<IHttpActionResult> DeleteItemCategory(string id)
        {
            ItemCategory itemCategory = await db.ItemCategories.FindAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            db.ItemCategories.Remove(itemCategory);
            await db.SaveChangesAsync();

            return Ok(itemCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemCategoryExists(string id)
        {
            return db.ItemCategories.Count(e => e.Id == id) > 0;
        }
    }
}