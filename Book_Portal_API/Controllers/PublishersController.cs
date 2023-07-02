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
using Book_Portal_API.Helpers;

namespace Book_Portal_API.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PubsContext _context;

        public PublishersController(PubsContext context)
        {
            _context = context;
        }

        // POST: api/publishers
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {

            _context.Publishers.Add(publisher);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PublisherExists(publisher.PubId))
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

        // GET: api/publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
          if (_context.Publishers == null)
          {
              return NotFound();
          }
            return await _context.Publishers.ToListAsync();
        }

        // GET: api/publishers/pubname/{name}
        [HttpGet("pubname/{name}")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishersByName(string name)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }
            var publishers = await this._context.Publishers.Where(p => p.PubName == name).ToListAsync();

            if(publishers == null)
            {
                return NotFound();
            }
            return publishers;
        }

        // GET: api/publishers/city/{city}
        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishersByCity(string city)
        {
            if(_context.Publishers == null)
            {
                return NotFound();
            }
            var publishers = await this._context.Publishers.Where(p => p.City == city).ToListAsync();

            if (publishers == null)
            {
                return NotFound();
            }
            return publishers;
        }


        // GET: api/publishers/state/{state}
        [HttpGet("state/{state}")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishersByState(string state)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }
            var publishers = await this._context.Publishers.Where(p => p.State == state).ToListAsync();

            if (publishers == null)
            {
                return NotFound();
            }
            return publishers;
        }

        // GET: api/publishers/country/{country}
        [HttpGet("country/{country}")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishersByCountry(string country)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }
            var publishers = await this._context.Publishers.Where(p => p.Country == country).ToListAsync();

            if (publishers == null)
            {
                return NotFound();
            }
            return publishers;
        }


        // GET: api/publishers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(string id)
        {
          if (_context.Publishers == null)
          {
              return NotFound();
          }
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        // PATCH: api/publishers/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPublisher(string id, JsonPatchDocument<Publisher> publisherPatch)
        {
            var publisher = await this._context.Publishers.FindAsync(id);

            if (publisher != null)
            {
                publisherPatch.ApplyTo(publisher);
                this._context.SaveChanges();

            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // PUT: api/publishers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(string id, Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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


        // DELETE: api/publishers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(string id)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // PATCH: api/pubinfo/{id}
        [HttpPatch("/api/pubinfo/{id}")]
        public async Task<IActionResult> PatchPubINfo(string id, JsonPatchDocument<PubInfo> pubInfoPatch)
        {
            var pubinfo = await this._context.PubInfos.FindAsync(id);

            if (pubinfo != null)
            {
                pubInfoPatch.ApplyTo(pubinfo);
                this._context.SaveChanges();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // PUT: /api/pubinfo/{id}
        [HttpPut("/api/pubinfo/{id}")]
        public async Task<IActionResult> PutPubInfo(string id, PubInfo pubInfo)
        {
            if (id != pubInfo.PubId)
            {
                return BadRequest();
            }

            _context.Entry(pubInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        private bool PublisherExists(string id)
        {
            return (_context.Publishers?.Any(e => e.PubId == id)).GetValueOrDefault();
        }
    }
}
