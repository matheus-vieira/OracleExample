namespace UI.Web.Services
{
    public class GenericApiService<TEntity> : IGenericApiService<TEntity> where TEntity : Models.BaseClass, new()
    {
        protected readonly System.Net.Http.HttpClient Client;

        protected string RouteUrl => new TEntity().RouteUrl;

        public GenericApiService()
        {
            Client = new System.Net.Http.HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public virtual async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TEntity>> GetAllAsync()
        {
            var response = await Client.GetAsync($"{RouteUrl}");

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert
                .DeserializeObject<System.Collections.Generic.IEnumerable<TEntity>>(result);

        }

        public virtual async System.Threading.Tasks.Task<TEntity> GetById(object id)
        {
            var response = await Client.GetAsync($"{RouteUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TEntity>(result);

        }

        public virtual async System.Threading.Tasks.Task<System.Uri> AddAsync(TEntity resource)
        {
            var response = await Client.PostAsync($"{RouteUrl}", new JsonContent(resource));
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public virtual async System.Threading.Tasks.Task<TEntity> UpdateAsync(TEntity resource, object id)
        {
            var response = await Client.PutAsync($"{RouteUrl}/{id}", new JsonContent(resource));
            response.EnsureSuccessStatusCode();

            // Deserialize the updated resource from the response body.

            var result = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TEntity>(result);
        }

        public virtual async System.Threading.Tasks.Task<System.Net.HttpStatusCode> DeleteAsync(object id)
        {
            var response = await Client.DeleteAsync($"{RouteUrl}/{id}");
            return response.StatusCode;
        }

        private class JsonContent : System.Net.Http.StringContent
        {
            public JsonContent(object obj) :
                base(Newtonsoft.Json.JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, "application/json")
            { }
        }
    }
}
