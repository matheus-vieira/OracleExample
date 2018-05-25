using System.Net.Http;

namespace UI.Web.Services
{
    public class GenericApiService<TEntity> : IGenericApiService<TEntity> where TEntity : Models.BaseClass, new()
    {
        protected readonly System.Net.Http.HttpClient Client;

        protected static string RouteUrl => new TEntity().RouteUrl;

        public GenericApiService()
        {
            Client = new System.Net.Http.HttpClient();
        }

        public virtual async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TEntity>> GetAllAsync()
        {
            var response = await Client.GetAsync($"{RouteUrl}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<System.Collections.Generic.IEnumerable<TEntity>>();
            return null;

        }

        public virtual async System.Threading.Tasks.Task<TEntity> GetById(object id)
        {
            var response = await Client.GetAsync($"{RouteUrl}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<TEntity>();
            return null;

        }

        public virtual async System.Threading.Tasks.Task<System.Uri> AddAsync(TEntity resource)
        {
            var response = await Client.PostAsJsonAsync($"{RouteUrl}", resource);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public virtual async System.Threading.Tasks.Task<TEntity> UpdateAsync(TEntity resource, object id)
        {
            var response = await Client.PutAsJsonAsync($"{RouteUrl}/{id}", resource);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated resource from the response body.
            resource = await response.Content.ReadAsAsync<TEntity>();
            return resource;
        }

        public virtual async System.Threading.Tasks.Task<System.Net.HttpStatusCode> DeleteAsync(object id)
        {
            var response = await Client.DeleteAsync($"{RouteUrl}/{id}");
            return response.StatusCode;
        }
    }
}
