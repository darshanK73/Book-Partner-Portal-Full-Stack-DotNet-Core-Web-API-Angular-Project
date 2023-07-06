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
using Book_Portal_API.Payloads;

namespace Book_Portal_API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly PubsContext _context;

        public AuthorsController(PubsContext context)
        {
            _context = context;
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult<string>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuthorExists(author.AuId))
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


        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("allId")]
        public async Task<ActionResult<IEnumerable<AuthorWithId>>> GetAllAuthorsId()
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            List<AuthorWithId> authorsIds = new List<AuthorWithId>();

            var authors = await _context.Authors.ToListAsync();

            foreach (var author in authors)
            {
                authorsIds.Add(new AuthorWithId()
                {
                    AuthorId = author.AuId,
                    AuthorName = author.AuFname + " " + author.AuLname
                });
            }
            return Ok(authorsIds);
        }

        // GET: api/authors/lname/{ln}
        [HttpGet("lname/{ln}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorByLastName(string ln)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var authors = await _context.Authors.Where(a => a.AuLname == ln).ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // GET: api/authors/fname/{fn}
        [HttpGet("fname/{fn}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorByFirstName(string fn)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var authors = await _context.Authors.Where(a => a.AuFname == fn).ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // GET: api/authors/phone/{phone}
        [HttpGet("phone/{phone}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorByPhone(string phone)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var authors = await _context.Authors.Where(a => a.Phone == phone).ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // GET: api/authors/zip/{zip}
        [HttpGet("zip/{zip}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorByZip(string zip)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var authors = await _context.Authors.Where(a => a.Zip == zip).ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // GET: api/authors/state/{state}
        [HttpGet("state/{state}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorByState(string state)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var authors = await _context.Authors.Where(a => a.State == state).ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // GET: api/authors/city/{city}
        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorByCity(string city)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var authors = await _context.Authors.Where(a => a.City == city).ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // GET: api/authors/id/{id}
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Author>> GetAuthor(string id)
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PATCH: api/authors/update/{id}
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> PatchAuthor(string id, JsonPatchDocument<Author> authorPatch)
        {

            var author = await _context.Authors.FindAsync(id);

            if (author != null)
            {
                authorPatch.ApplyTo(author);
                this._context.SaveChanges();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // PUT: api/authors/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutAuthor(string id, Author author)
        {
            if (id != author.AuId)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(string id)
        {
            return (_context.Authors?.Any(e => e.AuId == id)).GetValueOrDefault();
        }
    }
}
