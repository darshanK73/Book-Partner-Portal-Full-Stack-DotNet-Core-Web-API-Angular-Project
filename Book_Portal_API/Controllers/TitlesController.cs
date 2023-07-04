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
    [Authorize]
    [Route("api/titles")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly PubsContext _context;

        public TitlesController(PubsContext context)
        {
            _context = context;
        }

        // POST: api/titles
        [HttpPost]
        public async Task<ActionResult<string>> PostTitle(Title title)
        {
            _context.Titles.Add(title);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TitleExists(title.TitleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Recoard Created Successfully");
        }

        // GET: api/titles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitles()
        {
          if (_context.Titles == null)
          {
              return NotFound();
          }
            return await _context.Titles.ToListAsync();
        }

        // GET: api/titles/title/{title}
        [HttpGet("title/{title}")]
        public async Task<ActionResult<Title>> GetTitleByTitle(string title1)
        {
            if (_context.Titles == null)
            {
                return NotFound();
            }
            var title = await _context.Titles.Where(t => t.Title1 == title1).FirstOrDefaultAsync();

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }

        // GET: api/titles/type/{type}
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitleByType(string type)
        {
            if (_context.Titles == null)
            {
                return NotFound();
            }
            var titles = await _context.Titles.Where(t => t.Type == type).ToListAsync();

            if (titles == null)
            {
                return NotFound();
            }

            return titles;
        }

        // GET: api/titles/pubild/{pubid}
        [HttpGet("pubid/{pubid}")]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitlePubId(string pubid)
        {
            if (_context.Titles == null)
            {
                return NotFound();
            }
            var titles = await _context.Titles.Where(t => t.PubId == pubid).ToListAsync();

            if (titles == null)
            {
                return NotFound();
            }

            return titles;
        }

        // GET: api/titles/pubdate/{pubdate}
        [HttpGet("pubdate/{pubdate}")]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitlePubDate(DateTime pubdate)
        {
            if (_context.Titles == null)
            {
                return NotFound();
            }
            var titles = await _context.Titles.Where(t => t.Pubdate == pubdate).ToListAsync();

            if (titles == null)
            {
                return NotFound();
            }

            return titles;
        }

        // GET: api/titles/authorname/{authorname}
        [HttpGet("authorname/{authorname}")]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitleByAuthorName(string authorname)
        {
            if (_context.Titles == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.Where(a => a.AuFname == authorname).FirstOrDefaultAsync();

            var authortitle = await _context.Titleauthors.Where(at => at.Au == author).FirstOrDefaultAsync();

            var titles = await _context.Titles.Where(t => t.TitleId == authortitle.TitleId).ToListAsync();

            if (titles == null)
            {
                return NotFound();
            }

            return titles;
        }


        // GET: api/titles/top5titles
        [HttpGet("top5titles")]
        public async Task<ActionResult<IEnumerable<Title>>> GetTop5Titles()
        {
            if (_context.Titles == null)
            {
                return NotFound();
            }

            var titles = await this._context.Titles.OrderByDescending(t => t.YtdSales).Take(5).ToListAsync();

            return titles;
        }

        // GET: api/titles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Title>> GetTitle(string id)
        {
          if (_context.Titles == null)
          {
              return NotFound();
          }
            var title = await _context.Titles.FindAsync(id);

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }


        // PATCH: api/titles/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTitle(string id, JsonPatchDocument<Title> titlePatch)
        {
            var title = await this._context.Titles.FindAsync(id);

            if(title != null)
            {
                titlePatch.ApplyTo(title);
                this._context.SaveChanges();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // PUT: api/titles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTitle(string id, Title title)
        {
            if (id != title.TitleId)
            {
                return BadRequest();
            }

            _context.Entry(title).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleExists(id))
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

        // DELETE: api/titles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTitle(string id)
        {
            if (_context.Titles == null)
            {
                return NotFound();
            }
            var title = await _context.Titles.FindAsync(id);
            if (title == null)
            {
                return NotFound();
            }

            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        private bool TitleExists(string id)
        {
            return (_context.Titles?.Any(e => e.TitleId == id)).GetValueOrDefault();
        }
    }
}
