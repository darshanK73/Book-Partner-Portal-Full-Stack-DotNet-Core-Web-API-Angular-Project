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

namespace Book_Portal_API.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly PubsContext _context;

        public SalesController(PubsContext context)
        {
            _context = context;
        }

        // POST: api/sales
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
            _context.Sales.Add(sale);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SaleExists(sale.StorId))
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

        // GET: api/sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            return await _context.Sales.ToListAsync();
        }

        // GET: api/sales/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(string id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // GET: api/sales/titleid/{id}
        [HttpGet("titleid/{id}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSaleByTitleId(string id)
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            var sale = await _context.Sales.Where(s => s.TitleId == id).ToListAsync();

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // GET: api/sales/orderdate/{odate}
        [HttpGet("orderdate/{odate}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSaleByOrderDate(DateTime odate)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var sale = await _context.Sales.Where(s => s.OrdDate == odate).ToListAsync();

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // GET: api/sales/storeid/{id}
        [HttpGet("storeid/{id}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSaleByStoreId(string id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var sale = await _context.Sales.Where(s => s.StorId == id).ToListAsync();

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PATCH api/sales/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSale(string id, JsonPatchDocument<Sale> salesPatch)
        {
            var sales = await this._context.Sales.FindAsync(id);

            if (sales != null)
            {
                salesPatch.ApplyTo(sales);
                this._context.SaveChanges();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();

        }

        // PUT: api/sales/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(string id, Sale sale)
        {
            if (id != sale.StorId)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
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


        // DELETE: api/sales/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(string id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(string id)
        {
            return (_context.Sales?.Any(e => e.StorId == id)).GetValueOrDefault();
        }
    }
}
