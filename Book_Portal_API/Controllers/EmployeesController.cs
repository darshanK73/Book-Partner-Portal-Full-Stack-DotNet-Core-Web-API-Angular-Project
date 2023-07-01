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
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly PubsContext _context;

        public EmployeesController(PubsContext context)
        {
            _context = context;
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {

            _context.Employees.Add(employee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmpId))
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


        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            return await _context.Employees.ToListAsync();
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // GET: api/employees/pubid/{id}
        [HttpGet("pubid/{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByPubId(string id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employees = await _context.Employees.Where(e => e.PubId == id).ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        // GET: api/employees/fname/{fname}
        [HttpGet("fname/{fname}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByFname(string fname)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employees = await _context.Employees.Where(e => e.Fname == fname).ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        // GET: api/employees/lname/{lname}
        [HttpGet("lname/{lname}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByLname(string lname)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employees = await _context.Employees.Where(e => e.Lname == lname).ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        // GET: api/employees/hiredate/{hiredate}
        [HttpGet("hiredate/{hiredate}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByHireDate(DateTime hiredate)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employees = await _context.Employees.Where(e => e.HireDate == hiredate).ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        // PATCH: api/employees/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployee(string id, JsonPatchDocument<Employee> employeePatch)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                employeePatch.ApplyTo(employee);
                this._context.SaveChanges();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            
            return NoContent();
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(string id, Employee employee)
        {
            if (id != employee.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        
        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string id)
        {
            return (_context.Employees?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }
    }
}
