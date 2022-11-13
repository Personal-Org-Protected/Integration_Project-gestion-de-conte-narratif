using Application.Common.Interfaces;
using Domain.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.UserAuth0Client
{
    public class TokenAuth0Client : ITokenAuth0Client
    {
        private  readonly HttpClient _TokenClient;
        private  readonly IConfiguration _configuration;
        public TokenAuth0Client(HttpClient httpClient, IConfiguration configuration)
        {
            _TokenClient = httpClient;
            _configuration = configuration;
            _TokenClient.DefaultRequestHeaders
                                              .Accept
                                              .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }
        public  async Task<Token> FetchToken()//il faut changer les credentials
        {
            var stringData = $"grant_type=client_credentials&client_id={_configuration.GetSection("StoryTell_ClientId").Value}&client_secret={_configuration.GetSection("StoryTell_ClientSecret").Value}&audience={_configuration["Auth0:Audience:ManagementApi"]}";
            var response = await _TokenClient.PostAsync("https://dev-dlblb59r.us.auth0.com/oauth/token", new StringContent(stringData, Encoding.Default, "application/x-www-form-urlencoded"));
            response.EnsureSuccessStatusCode();
            var Data = await response.Content.ReadAsStringAsync();
            var token=JsonConvert.DeserializeObject<Token>(Data);
            return token;
        }
    }
}
