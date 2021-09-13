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
using Microsoft.Extensions.Configuration;

namespace TestKhoaExample.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IConfiguration configuration;
        public AccountRepository(
            IHttpContextAccessor _httpContext,
            IConfiguration _configuration
            )
        {
            httpContext = _httpContext;
            configuration = _configuration;
        }
        public bool IsSigIn()
        {
            return httpContext.HttpContext.Request.Cookies["User"] != null;
        }
        public async Task<List<Role>> LoadListRoles()
        {
            var client = new RestClient(LinkAPI() + "/api/account/getroles?strUser=" + User())
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + Token());
            request.AlwaysMultipartFormData = true;
            IRestResponse response = await client.ExecuteAsync(request);
            
            return JsonConvert.DeserializeObject<List<Role>>(response.Content);
        }
        public async Task<List<AccountModel>> LoadListUser()
        {
            var client = new RestClient(LinkAPI() + "/api/account/ListUser?strUser=" + User())
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + Token());
            request.AlwaysMultipartFormData = true;
            IRestResponse response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<List<AccountModel>>(response.Content);
        }
        public async Task<IndexViewModel> LoadDetail()
        {
            var client = new RestClient(LinkAPI() + "/api/account/getdetailuser?strUser=" + User())
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + Token());
            IRestResponse response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<IndexViewModel>(response.Content);
        }
        public string GetRoleName()
        {
            return GetInfo(1);
        }
        public string GetUserId()
        {
            return GetInfo(2);
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
        private string LinkAPI() => configuration.GetSection("LINK_API").Value;
        private string Token() => httpContext.HttpContext.Request.Cookies["uToken"];
        private string User() => httpContext.HttpContext.Request.Cookies["User"];
        //private JwtSecurityToken GetInfo(string strToken)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(strToken);
        //    return (JwtSecurityToken)jsonToken;
        //}
    }
}
