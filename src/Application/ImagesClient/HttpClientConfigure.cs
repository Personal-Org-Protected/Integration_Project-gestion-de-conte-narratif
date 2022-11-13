using Application.Common.Interfaces;
using Application.ImagesClient.Queries;
using Newtonsoft.Json;
using Polly;
using Polly.Registry;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ImagesClient
{
    public  class HttpClientConfigure : IImageSearchClient
    {
        //private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;
        private readonly HttpClient _httpClient;
        public HttpClientConfigure(HttpClient client, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            _retryPolicy = policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>("RetryPolicyImageClient");
            _httpClient = client;
        }
        public async Task<HttpResponseMessage> ConfigureClient(string querySearch,int pageNumber,int pageSize,bool autoCorrect)
        {
			string url = $"https://contextualwebsearch-websearch-v1.p.rapidapi.com/api/Search/ImageSearchAPI?q={ConfigureQuerySearch(querySearch)}&pageNumber={pageNumber}&pageSize={pageSize}&autoCorrect={autoCorrect}";
            var response = await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _httpClient.GetAsync(url);
            });
            return response;
		}

		private string ConfigureQuerySearch(string query)
        {
			string queryString = query;
            var value = query.Split(' ');
			if (value.Length > 1)
			{
               queryString = "";
                foreach (var item in value)
                {
                    if (value.Last() != item)
                        queryString += item + "%20";
                    else queryString += item;
                }
			}
			return queryString;
        }
        public async Task<ImageClientVM> RetrieveDataClientWeb(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<ImageClientVM>(body);
            return task;
        }


        //private async Task<ImageClientVM> RetryCallClientPolicy(HttpClient clientImage,string url/*, HttpRequestMessage request*/)
        //{
        //    var response= await _retryPolicy.ExecuteAsync(async () =>
        //    {
        //        return await clientImage.GetAsync(url);//.SendAsync(request);
        //    });
        //    response.EnsureSuccessStatusCode();
        //    var body = await response.Content.ReadAsStringAsync();
        //    var task = JsonConvert.DeserializeObject<ImageClientVM>(body);
        //    return task;
        //}
    }
}
