using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Book_Portal_API.Models;

namespace Book_Portal_API.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly PubsContext _context;

        public JobsController(PubsContext context)
        {
            _context = context;
        }

        // POST: api/jobs
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        { 
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return Ok("Record Created Successfully");
        }

        // GET: api/jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
          if (_context.Jobs == null)
          {
              return NotFound();
          }
            return await _context.Jobs.ToListAsync();
        }

        // GET: api/jobs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(short id)
        {
          if (_context.Jobs == null)
          {
              return NotFound();
          }
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // GET: api/jobs/minval/{minval}
        [HttpGet("minval/{minval}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobByMinVal(byte minval)
        {
            if (_context.Jobs == null)
            {
                return NotFound();
            }
            var jobs = await _context.Jobs.Where(j => j.MinLvl == minval).ToListAsync();

            if (jobs == null)
            {
                return NotFound();
            }

            return jobs;
        }

        // GET: api/jobs/maxval/{maxval}
        [HttpGet("maxval/{maxval}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobByMaxVal(byte maxval)
        {
            if (_context.Jobs == null)
            {
                return NotFound();
            }
            var jobs = await _context.Jobs.Where(j => j.MinLvl == maxval).ToListAsync();

            if (jobs == null)
            {
                return NotFound();
            }

            return jobs;
        }


        // PUT: api/jobs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(short id, Job job)
        {
            if (id != job.JobId)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

       

        // DELETE: api/jobs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(short id)
        {
            if (_context.Jobs == null)
            {
                return NotFound();
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(short id)
        {
            return (_context.Jobs?.Any(e => e.JobId == id)).GetValueOrDefault();
        }
    }
}
