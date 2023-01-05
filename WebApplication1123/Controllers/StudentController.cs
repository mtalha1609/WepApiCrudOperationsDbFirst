using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1123.Models;

namespace WebApplication1123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly wapiContext _context;
        public StudentController(wapiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var hero = await _context.Students.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Record Not Found");
            }
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent(Student std)
        {
            _context.Students.Add(std);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student request)
        {

            var dbstd = await _context.Students.FindAsync(request.Id);
            if (dbstd == null)
                return BadRequest("Student Not Found");
            dbstd.Name = request.Name;
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Student>>> Delete(int id)
        {
            var dbstd = await _context.Students.FindAsync(id);
            if (dbstd == null)
            
                return BadRequest("Record Not Found");
            _context.Students.Remove(dbstd);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }


    }
}
