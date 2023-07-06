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
using Book_Portal_API.Payloads;
using Microsoft.AspNetCore.SignalR;

namespace Book_Portal_API.Controllers
{
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
        public async Task<ActionResult<string>> PostTitle(TitlePublishRequest titleRequest)
        {
            if(titleRequest == null)
            {
                return BadRequest();
            }
            
            var pub = await _context.Publishers.FindAsync(titleRequest.PubId);

            if(pub == null)
            {
                return BadRequest(new { Message = "Publisher Do Not Exists" });
            }

            var titleauthors = new List<Titleauthor>();

            var bookId = CreateTitleId(titleRequest.Type);

            foreach(var auid in titleRequest.AuIds)
            {
                var author = await _context.Authors.Where(au => au.AuId == auid).FirstOrDefaultAsync();
                if(author == null)
                {
                    return BadRequest(new { Message = "Author with given id not found" });
                }
                titleauthors.Add(new Titleauthor()
                {
                    AuId = auid,
                    TitleId = bookId,
                    AuOrd = titleRequest.AuOrd,
                    Royaltyper = titleRequest.Royaltyper,
                });

            }

            Title title = new Title()
            {
                TitleId = bookId,
                Title1 = titleRequest.Title1,
                Type = titleRequest.Type,
                PubId = titleRequest.PubId,
                Price = titleRequest.Price,
                Advance = titleRequest.Advance,
                Royalty = titleRequest.Royalty,
                YtdSales = titleRequest.YtdSales,
                Notes = titleRequest.Notes,
                Pubdate = titleRequest.Pubdate,
                Pub = pub,
                Titleauthors = titleauthors,
                Sales = new List<Sale>()
            };

            await _context.Titles.AddAsync(title);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Recoard Created Successfully" });
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
        public async Task<ActionResult<TitleResponse>> GetTitle(string id)
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

            List<string> authorIds = new List<string>();
            List<byte?> auords = new List<byte?>();
            List<int?> typer = new List<int?>();
            var titleauthors =  await _context.Titleauthors.Where(ta => ta.TitleId == title.TitleId).ToListAsync();
            foreach( var author in titleauthors)
            {
                auords.Add(author.AuOrd);
                authorIds.Add(author.AuId);
                typer.Add(author.Royaltyper);
            }

            var pub = await _context.Publishers.FindAsync(title.PubId);

            var titleRequest = new TitleResponse()
            {
                Title1 = title.Title1,
                Type = title.Type,
                PubId = title.PubId,
                Price = title.Price,
                Advance = title.Advance,
                Royalty = title.Royalty,
                YtdSales = title.YtdSales,
                Notes = title.Notes,
                Pubdate = title.Pubdate,
                AuIds = authorIds.ToArray(),
                AuOrd = auords.ToArray(),
                Royaltyper = typer.ToArray()

                
            };

            return Ok(titleRequest);
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
        public async Task<IActionResult> PutTitle(string id, TitlePublishRequest titleRequest)
        {
            if (titleRequest == null)
            {
                return BadRequest();
            }

            var title = await _context.Titles.FindAsync(id);

            if(title == null)
            {
                return BadRequest();
            }

            var pub = await _context.Publishers.FindAsync(titleRequest.PubId);

            if (pub == null)
            {
                return BadRequest(new { Message = "Publisher Do Not Exists" });
            }

            var titleauthors = new List<Titleauthor>();


            foreach (var auid in titleRequest.AuIds)
            {
                var author = await _context.Authors.Where(au => au.AuId == auid).FirstOrDefaultAsync();
                if (author == null)
                {
                    return BadRequest(new { Message = "Author with given id not found" });
                }
                titleauthors.Add(new Titleauthor()
                {
                    AuId = auid,
                    TitleId = id,
                    AuOrd = titleRequest.AuOrd,
                    Royaltyper = titleRequest.Royaltyper,
                });

            }

            title.Title1 = titleRequest.Title1;
            title.Type = titleRequest.Type;
            title.PubId = titleRequest.PubId;
            title.Price = titleRequest.Price;
            title.Advance = titleRequest.Advance;
            title.Royalty = titleRequest.Royalty;
            title.YtdSales = titleRequest.YtdSales;
            title.Notes = titleRequest.Notes;
            title.Pubdate = titleRequest.Pubdate;
            title.Pub = pub;
            title.Titleauthors = titleauthors;

            await _context.Titles.AddAsync(title);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Recoard Created Successfully" });
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

        private string CreateTitleId(string type)
        {
            Random rd = new Random();
            return type.Substring(0,2) +  rd.Next(1000, 9999).ToString();
        }
    }
}
