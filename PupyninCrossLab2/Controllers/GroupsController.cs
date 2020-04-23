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
    public class GroupsController : ControllerBase
    {
        private readonly ExampleContext _context;

        public GroupsController(ExampleContext context)
        {
            _context = context;
        }

        // GET: api/Group
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetGroupEntities()
        {
          
            return Ok(_context.Groups.ToList());
        }

        [Authorize]
        // GET: api/Group/5
        [HttpGet("{id}")]
        public ActionResult<GroupEntity> GetGroup(int id)
        {
            var groups = _context.Groups.ToList();
            var group = groups.Where(tmp => tmp.Id == id).FirstOrDefault();
            var students = _context.Students.ToList();

            var sortedStudents = students.Where(tmp => tmp.GroupId == group.Id).ToList();

            group.Students = sortedStudents;
          

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // PUT: api/Group/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult PutGroup(int id, GroupEntity group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            _context.Entry(group).State = EntityState.Modified;

            try
            {
                 _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupEntityExists(id))
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

        // POST: api/Group
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult<GroupEntity> PostGroup(GroupEntity group)
        {
            _context.Groups.Add(group);
            _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = group.Id }, group);
        }

        // DELETE: api/Group/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<GroupEntity> DeleteGroup(int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            _context.SaveChangesAsync();

            return group;
        }

        private bool GroupEntityExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
