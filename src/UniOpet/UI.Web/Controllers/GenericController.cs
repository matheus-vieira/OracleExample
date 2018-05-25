namespace UI.Web.Controllers
{
    public class GenericController<TEntity> : Microsoft.AspNetCore.Mvc.Controller where TEntity : class
    {
        protected readonly Services.IGenericApiService<TEntity> Service;

        public GenericController(Services.IGenericApiService<TEntity> service)
        {
            Service = service;
        }

        // GET: TEntity
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Index() =>
            View(await GetAll());

        // GET: TEntity/Details/5
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var resource = await GetById(id.Value);

            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // GET: TEntity/Create
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Create()
        {
            await AddViewData();
            return View();
        }

        // POST: TEntity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Create(TEntity resource)
        {
            if (!ModelState.IsValid)
                return View(resource);

            await Service.AddAsync(resource);
            return RedirectToAction(nameof(Index));
        }

        // GET: TEntity/Edit/5
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Edit(object id)
        {
            if (id == null)
                return NotFound();

            var resource = await GetById(id);

            if (resource == null)
                return NotFound();

            await AddViewData(resource);

            return View(resource);
        }

        // POST: TEntity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Edit(object id, TEntity resource)
        {
            if (!ModelState.IsValid)
                return View(resource);

            try
            {
                await Service.UpdateAsync(resource, id);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!await Exists(id))
                    return NotFound();

                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TEntity/Delete/5
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var resource = await GetById(id.Value);
            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // POST: TEntity/Delete/5
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> DeleteConfirmed(int id)
        {
            if (!await Exists(id))
                return NotFound();

            await Service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        protected virtual async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TEntity>> GetAll() => await Service.GetAllAsync();

        protected virtual async System.Threading.Tasks.Task<bool> Exists(object id) => await GetById(id) != null;

        protected virtual async System.Threading.Tasks.Task<TEntity> GetById(object id) => await Service.GetById(id);

        protected virtual async System.Threading.Tasks.Task<object> AddViewData(TEntity resource = null)
            => await System.Threading.Tasks.Task.FromResult(default(object));
    }
}