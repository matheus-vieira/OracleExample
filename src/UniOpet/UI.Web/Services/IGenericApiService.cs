namespace UI.Web.Services
{
    public interface IGenericApiService<TEntity> where TEntity : class
    {
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TEntity>> GetAllAsync();
        System.Threading.Tasks.Task<TEntity> GetById(object id);
        System.Threading.Tasks.Task<System.Uri> AddAsync(TEntity resource);
        System.Threading.Tasks.Task<TEntity> UpdateAsync(TEntity resource, object id);
        System.Threading.Tasks.Task<System.Net.HttpStatusCode> DeleteAsync(object id);
    }
}
