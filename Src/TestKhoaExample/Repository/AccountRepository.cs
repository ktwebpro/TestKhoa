using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TestKhoaExample.Models;
using TestKhoaExample.IRepository;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using TestKhoaExample.CustomModels;
using RestSharp;
using Newtonsoft.Json;

namespace TestKhoaExample.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpContextAccessor httpContext;
        public AccountRepository(
            IHttpContextAccessor _httpContext
            )
        {
            httpContext = _httpContext;
        }
        public bool IsSigIn()
        {
            return httpContext.HttpContext.Request.Cookies["User"] != null;
        }
        public async Task<List<Role>> LoadListRoles()
        {
            string token = httpContext.HttpContext.Request.Cookies["User"];
            var client = new RestClient("https://localhost:44314/api/account/getroles")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = await client.ExecuteAsync(request);
            
            return JsonConvert.DeserializeObject<List<Role>>(response.Content);
        }
        public async Task<List<AccountModel>> LoadListUser()
        {
            string token = httpContext.HttpContext.Request.Cookies["User"];
            var client = new RestClient("https://localhost:44314/api/account/ListUser")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = await client.ExecuteAsync(request);
            
            return JsonConvert.DeserializeObject<List<AccountModel>>(response.Content);
        }
        public async Task<IndexViewModel> LoadDetail()
        {
            string token = httpContext.HttpContext.Request.Cookies["User"];
            var client = new RestClient("https://localhost:44314/api/account/getdetailuser")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<IndexViewModel>(response.Content);
        }
        public string GetRoleName()
        {
            return GetInfo(1);
        }
        public string GetUserId()
        {
            return "0";
        }
        public string GetUserName()
        {
            return GetInfo(0);
        }
        private string GetInfo(int iOrder)
        {
            if (!IsSigIn()
                    || string.IsNullOrEmpty(httpContext.HttpContext.Request.Cookies["User"].ToString())
                    || !httpContext.HttpContext.Request.Cookies["User"].Contains("|"))
                return "";
            else
            {
                string[] arrayUserInfo = httpContext.HttpContext.Request.Cookies["User"].Split("|");
                return arrayUserInfo[iOrder];
            }
        }
        //private JwtSecurityToken GetInfo(string strToken)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(strToken);
        //    return (JwtSecurityToken)jsonToken;
        //}
    }
}
