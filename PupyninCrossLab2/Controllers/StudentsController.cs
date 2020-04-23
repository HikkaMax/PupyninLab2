using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupyninLab2.Entity;

namespace PupyninLab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ExampleContext _context;

        public StudentsController(ExampleContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetStudents()
        {
            var students = _context.Students.ToList();

            return Ok(_context.Students.ToList());
        }

        // GET: api/Students/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<StudentEntity> GetStudent(int id)
        {

            var students = _context.Students.ToList();
       
            var student = students.Where(tmp => tmp.Id == id).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, StudentEntity student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult<StudentEntity> PostStudent(StudentEntity student)
        {
            _context.Students.Add(student);
            _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public ActionResult<StudentEntity> DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
