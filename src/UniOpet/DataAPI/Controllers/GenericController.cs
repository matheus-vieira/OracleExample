using System.Data.Entity;

namespace DataAPI.Controllers
{
    public abstract class GenericController<TEntity> : System.Web.Http.ApiController where TEntity : class
    {
        protected readonly Context.UniOpetDbContext Database;
        private readonly System.Data.Entity.DbSet<TEntity> _dbSet;

        protected GenericController()
        {
            Database = new Context.UniOpetDbContext();
            _dbSet = Database.Set<TEntity>();
        }

        // GET: api/Resource
        public virtual async System.Threading.Tasks.Task<System.Collections.Generic.ICollection<TEntity>> Get()
        {
            return await GetAll();
        }

        // GET: api/Resource/5
        public virtual async System.Threading.Tasks.Task<System.Web.Http.IHttpActionResult> Get(object id)
        {
            var resource = await _dbSet.FindAsync(id);
            if (resource == null)
                return NotFound();

            return Ok(resource);
        }

        // PUT: api/Resource/5
        [System.Web.Http.Description.ResponseType(typeof(void))]
        public virtual async System.Threading.Tasks.Task<System.Web.Http.IHttpActionResult> Put(object id, TEntity resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != GetIdentifier(resource))
                return BadRequest();

            Database.Entry(resource).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await Database.SaveChangesAsync();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                if (!await Exists(resource))
                    return NotFound();
                throw;
            }

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        // POST: api/Resource
        //[ResponseType(typeof(TEntity))]
        public virtual async System.Threading.Tasks.Task<System.Web.Http.IHttpActionResult> Post(TEntity resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _dbSet.Add(resource);
            await Database.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = GetIdentifier(resource) }, resource);
        }

        // DELETE: api/Resource/5
        //[ResponseType(typeof(Student))]
        public virtual async System.Threading.Tasks.Task<System.Web.Http.IHttpActionResult> Delete(object id)
        {
            var resource = await GetById(id);
            if (resource == null)
                return NotFound();

            _dbSet.Remove(resource);
            await Database.SaveChangesAsync();

            return Ok(resource);
        }

        private async System.Threading.Tasks.Task<TEntity> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Database?.Dispose();

            base.Dispose(disposing);
        }

        protected async System.Threading.Tasks.Task<System.Collections.Generic.ICollection<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        protected virtual async System.Threading.Tasks.Task<bool> Exists(TEntity resource)
        {
            return await _dbSet.FindAsync(GetIdentifier(resource)) != null;
        }

        protected abstract object GetIdentifier(TEntity resource);
    }
}