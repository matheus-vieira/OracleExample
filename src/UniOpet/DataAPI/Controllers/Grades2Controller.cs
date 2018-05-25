using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DataAPI.Context;
using DataAPI.Models;

namespace DataAPI.Controllers
{
    public class Grades2Controller : ApiController
    {
        protected readonly UniOpetDbContext Db;
        protected readonly DbSet<Grade> ResourceDbSet;

        public Grades2Controller()
        {
            Db = new UniOpetDbContext();
            ResourceDbSet = Db.Set<Grade>();
        }

        // GET: api/Grades2
        public IQueryable<Grade> Get()
        {
            return ResourceDbSet;
        }

        // GET: api/Grades2/5
        [ResponseType(typeof(Grade))]
        public async Task<IHttpActionResult> GetGrade(int id)
        {
            Grade grade = await ResourceDbSet.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        // PUT: api/Grades2/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGrade(int id, Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grade.Id)
            {
                return BadRequest();
            }

            Db.Entry(grade).State = EntityState.Modified;

            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Grades2
        [ResponseType(typeof(Grade))]
        public async Task<IHttpActionResult> PostGrade(Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResourceDbSet.Add(grade);
            await Db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = grade.Id }, grade);
        }

        // DELETE: api/Grades2/5
        [ResponseType(typeof(Grade))]
        public async Task<IHttpActionResult> DeleteGrade(int id)
        {
            Grade grade = await ResourceDbSet.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            ResourceDbSet.Remove(grade);
            await Db.SaveChangesAsync();

            return Ok(grade);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GradeExists(int id)
        {
            return Db.Grades.Count(e => e.Id == id) > 0;
        }
    }
}