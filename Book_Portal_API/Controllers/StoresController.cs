using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Book_Portal_API.Models;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace Book_Portal_API.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly PubsContext _context;

        public StoresController(PubsContext context)
        {
            _context = context;
        }

        // POST: api/stores
        [HttpPost]
        public async Task<ActionResult<string>> PostStore(Store store)
        {
            _context.Stores.Add(store);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StoreExists(store.StorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Record Created Successfully");
        }

        // GET: api/stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
          if (_context.Stores == null)
          {
              return NotFound();
          }
            return await _context.Stores.ToListAsync();
        }

        // GET: api/stores
        [HttpPost("titlesIds")]
        public async Task<ActionResult<IEnumerable<Store>>> GetStoresSellingTitle(string[] titlesIds)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.Where(sa => titlesIds.Contains(sa.TitleId)).ToListAsync();

            List<Store> stores = new List<Store>();

            foreach(var s in sales)
            {
                var st = await _context.Stores.Where(st => st.StorId == s.StorId).FirstOrDefaultAsync();
                if (!stores.Contains(st))
                {
                    stores.Add(st);
                }
            }

            return Ok(stores);
        }

        // GET : api/stores/name/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<Store>>> GetStoreByName(string name)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var stores = await _context.Stores.Where(s => s.StorName == name).ToListAsync();

            if(stores == null)
            {
                return NotFound();
            }

            return stores;
        }

        // GET : api/stores/city/{city}
        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<Store>>> GetStoresByCity(string city)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var stores = await _context.Stores.Where(s => s.City == city).ToListAsync();

            if (stores == null)
            {
                return NotFound();
            }

            return stores;
        }

        // GET : api/stores/zip/{zip}
        [HttpGet("zip/{zip}")]
        public async Task<ActionResult<IEnumerable<Store>>> GetStoresByZip(string zip)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var stores = await _context.Stores.Where(s => s.Zip == zip).ToListAsync();

            if (stores == null)
            {
                return NotFound();
            }

            return stores;
        }

        // GET: api/stores/id/{id}
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Store>> GetStore(string id)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }


        // PATCH: api/stores/update/{id}
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> PatchStore(string id, JsonPatchDocument<Store> storePatch)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store != null)
            {
                storePatch.ApplyTo(store);
                this._context.SaveChanges();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // PUT: api/sores/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutStore(string id, Store store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/stores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(string id)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(string id)
        {
            return (_context.Stores?.Any(e => e.StorId == id)).GetValueOrDefault();
        }
    }
}
