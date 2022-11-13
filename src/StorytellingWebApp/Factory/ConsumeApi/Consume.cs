using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using StorytellingWebApp.Models;
using System.Net.Http.Headers;
using System.Text;

namespace StorytellingWebApp.Factory.ConsumeApi
{
    public class Consume<T> : IConsume<T> where T : class
    {
        private HttpClient _httpClient;
        public Consume(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contextAccessor.HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult());
        }

        public async Task Delete(string chemin)
        {
           var response=await _httpClient.DeleteAsync(_httpClient.BaseAddress + chemin);
            response.EnsureSuccessStatusCode();
        }

        public async Task<PaginatedItems<T>> GetAll(string chemin)
        {
            var response=await _httpClient.GetAsync(_httpClient.BaseAddress + chemin);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<PaginatedItems<T>>(data);//?? throw
            return task;
        }
        public async Task<T> GetAllClient(string chemin)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + chemin);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<T>(data);
            return task;
        }
        public async Task<T> GetAll()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<T>(data);
            return task;
        }


        public async Task<T> GetById(string chemin)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress+chemin);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<T>(data);
            return task;
        }

        public async Task<T> Insert(T model)
        {
            var JsonData = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress,new StringContent(JsonData, Encoding.Default, "application/json"));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<T>(data);
            return task;
        }
        public async Task<Result> InsertWithResult(T model)
        {
            var JsonData = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress, new StringContent(JsonData, Encoding.Default, "application/json"));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<Result>(data);
            return task;
        }

        public async Task<T> Update(string chemin, T model)
        {
            var JsonData = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync(_httpClient.BaseAddress+chemin, new StringContent(JsonData, Encoding.Default, "application/json"));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<T>(data);
            return task;
        }

        public string ImplementPath(IDictionary<string, int> parameters)
        {
            var items = parameters.GetEnumerator();
            var sb = new StringBuilder();
            var isNext = items.MoveNext();
            while (isNext)
            {
                sb.Insert(0, $"?{items.Current.Key}={items.Current.Value}");isNext = items.MoveNext();
                if(isNext)sb.Append("&");
            }
            parameters.Clear();
            return sb.ToString();
        }
    }

}
