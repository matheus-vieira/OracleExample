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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using DataAPI.Context;
using DataAPI.Models;

namespace DataAPI.Controllers
{
    /*
    A classe WebApiConfig pode requerer alterações adicionais para adicionar uma rota para esse controlador.
    Misture essas declarações no método Register da classe WebApiConfig conforme aplicável. Note que URLs OData diferenciam maiúsculas e minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DataAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Grade>("Grades1");
    builder.EntitySet<Student>("Students"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Grades1Controller : ODataController
    {
        protected readonly UniOpetDbContext Db;
        protected readonly DbSet<Grade> ResourceDbSet;

        public Grades1Controller()
        {
            Db = new UniOpetDbContext();
            ResourceDbSet = Db.Set<Grade>();
        }

        // GET: odata/Grades1
        [EnableQuery]
        public IQueryable<Grade> GetGrades1()
        {
            return ResourceDbSet;
        }

        // GET: odata/Grades1(5)
        [EnableQuery]
        public SingleResult<Grade> GetGrade([FromODataUri] int key)
        {
            return SingleResult.Create(ResourceDbSet.Where(grade => grade.Id == key));
        }

        // PUT: odata/Grades1(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Grade> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grade = await ResourceDbSet.FindAsync(key);
            if (grade == null)
            {
                return NotFound();
            }

            patch.Put(grade);

            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(grade);
        }

        // POST: odata/Grades1
        public async Task<IHttpActionResult> Post(Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResourceDbSet.Add(grade);
            await Db.SaveChangesAsync();

            return Created(grade);
        }

        // PATCH: odata/Grades1(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Grade> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Grade grade = await ResourceDbSet.FindAsync(key);
            if (grade == null)
            {
                return NotFound();
            }

            patch.Patch(grade);

            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(grade);
        }

        // DELETE: odata/Grades1(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Grade grade = await ResourceDbSet.FindAsync(key);
            if (grade == null)
            {
                return NotFound();
            }

            ResourceDbSet.Remove(grade);
            await Db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Grades1(5)/Students
        [EnableQuery]
        public IQueryable<Student> GetStudents([FromODataUri] int key)
        {
            return ResourceDbSet.Where(m => m.Id == key).SelectMany(m => m.Students);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GradeExists(int key)
        {
            return ResourceDbSet.Count(e => e.Id == key) > 0;
        }
    }
}
