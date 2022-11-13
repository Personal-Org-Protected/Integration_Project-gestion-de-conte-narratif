using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.UserAuth0Client
{
    public class Auth0Client<T> : IAuth0Client<T> where T : class
    {
        private readonly ITokenAuth0Client _tokenClient;
        private  readonly HttpClient _UserClient;
        private readonly string url;
        private static DateTime timeSpan;
        public Auth0Client(HttpClient httpClient,IConfiguration configuration,ITokenAuth0Client tokenAuth0)
        {
             url = configuration["Auth0:Authority"] + "api/v2/users";
            _UserClient=httpClient;
            _tokenClient=tokenAuth0;
            timeSpan=DateTime.Now;
        }

        private async Task FetchAuthorization()
        {
            var isthere = _UserClient.DefaultRequestHeaders.Contains("Authorization");
            if (!isthere || ExpiredToken(timeSpan))
            {
                var token = await _tokenClient.FetchToken();
                TokenExpireTime(token.expires_in);
                _UserClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.access_token}");
            }
        }

        private void TokenExpireTime(double time)
        {
           timeSpan = DateTime.Now.AddSeconds(time);
        }

        private bool ExpiredToken(DateTime time)
        {
            if(DateTime.Now>=time)return true;
            return false;
        }
        public async Task<Result> CreateUserAsync(T user)
        {
            try
            {
                await FetchAuthorization();
                var userdata = JsonConvert.SerializeObject(user);
                var responses = await _UserClient.PostAsync(url, new StringContent(userdata, Encoding.Default, "application/json"));
                responses.EnsureSuccessStatusCode();
                var data = await responses.Content.ReadAsStringAsync();
                return Result.Success(data);
            }
            catch (Exception ex)
            {
                var errors = ex.Data.Values.Cast<string>();
                return Result.Failure("Creation auth FAILDED",errors);
            }
        }


        public async Task<Result> UpdateUserAsync(T user,string user_id)
        {
            try
            {
                await FetchAuthorization();
                var id = $"auth0|{user_id}";
                var userdata = JsonConvert.SerializeObject(user);
                var responses = await _UserClient.PatchAsync(url + $"/{id}", new StringContent(userdata, Encoding.Default, "application/json"));
                responses.EnsureSuccessStatusCode();
                var data = await responses.Content.ReadAsStringAsync();
                return Result.Success(data);

            }
            catch (Exception ex)
            {
                var errors = ex.Data.Values.Cast<string>();
                return Result.Failure("Update auth FAILDED", errors);
            }

        }
        public async Task<Result> DeleteUserAsync(string userId)
        {
            try
            {
                await FetchAuthorization();
                var id = $"auth0|{userId}";
                var responses = await _UserClient.DeleteAsync(url + $"/{id}");
                responses.EnsureSuccessStatusCode();
                var data = await responses.Content.ReadAsStringAsync();
                return Result.Success(data);
            }
            catch (Exception ex)
            {
                var errors = ex.Data.Values.Cast<string>();
                return Result.Failure("Delete auth FAILDED", errors);
            }
        }


        public async Task<Result> AddressingRole(string user_id, string RoleId)
        {
            var newUrl = url.Replace("users", "roles");
            var baseUrl = newUrl + $"/{RoleId}/users";
            try
            {
                await FetchAuthorization();
                var userdata = JsonConvert.SerializeObject(ConfigRoleUser(user_id));
                var responses = await _UserClient.PostAsync(baseUrl, new StringContent(userdata, Encoding.Default, "application/json"));
                responses.EnsureSuccessStatusCode();
                var data = await responses.Content.ReadAsStringAsync();
                return Result.Success(data);
            }
            catch (Exception ex)
            {
                var errors = ex.Data.Values.Cast<string>();
                return Result.Failure("addressing role auth FAILDED", errors);
            }
        }

        private AssignRoleUser ConfigRoleUser(string user_id)
        {
            var userAuthId = "auth0|" + user_id;//le mettre dans auth
            var userAssigned = new AssignRoleUser()
            {
                users = new List<string>()
            };
            userAssigned.users.Add(userAuthId);
            return userAssigned;
        }
        /*{"roles":["rol_leIr5SqIBpMcjGn2"]}*/
        public async Task<Result> DeleteRoleFromUser(string user_id,string role_id)
        {
            var userAuthId = "auth0|" + user_id;
            var baseUrl = url + $"/{userAuthId}/roles";
            try
            {
                await FetchAuthorization();
                var userdata = JsonConvert.SerializeObject(ConfigRoleToDelete(role_id));
                var responses = await _UserClient.SendAsync(configRequest(baseUrl, userdata)); //_UserClient.DeleteAsync(baseUrl, new StringContent(userdata, Encoding.Default, "application/json"));
                responses.EnsureSuccessStatusCode();
                var data = await responses.Content.ReadAsStringAsync();
                return Result.Success(data);
            }
            catch (Exception ex)
            {
                var errors = ex.Data.Values.Cast<string>();
                return Result.Failure("Deleting role auth from user FAILDED", errors);
            }
        }
        

        private HttpRequestMessage configRequest(string url,string? userdata)
        {
            var request = new HttpRequestMessage()
            {
               RequestUri=new Uri(url),
               Method = HttpMethod.Delete,
               Content= new StringContent(userdata, Encoding.Default, "application/json")
            };
            return request;
        }
        private DeleteRoleUser ConfigRoleToDelete(string role_id)
        {
            var rolesToDelete = new DeleteRoleUser()
            {
                roles = new List<string>()
            };
            rolesToDelete.roles.Add(role_id);
            return rolesToDelete;
        }

    }
}
