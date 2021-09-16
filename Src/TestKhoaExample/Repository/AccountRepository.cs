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
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
            return GetUserId() != "";
        }
        public async Task<List<Role>> LoadListRoles()
        {
            var client = new RestClient(LinkAPI() + "/api/account/getroles")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + Token());
            request.AlwaysMultipartFormData = true;
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode.ToString().Contains("Unauthorized"))
            {
                httpContext.HttpContext.Response.Redirect("/Home/Error?errcode=" + "Bạn không có quyền truy cập");
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Role>>(response.Content);
            }
        }
        public async Task<List<AccountModel>> LoadListUser()
        {
            var client = new RestClient(LinkAPI() + "/api/account/ListUser")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + Token());
            request.AlwaysMultipartFormData = true;
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode.ToString().Contains("Unauthorized"))
            {
                httpContext.HttpContext.Response.Redirect("/Account/AccessDenied");
                return null;
            }
            else
            {
                if (response.Content.Contains("err:"))
                {
                    httpContext.HttpContext.Response.Redirect("/Home/Error?err=" + response.Content.Replace("err:",""));
                    return null;
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<AccountModel>>(response.Content);
                }
            }
        }
        public async Task<IndexViewModel> LoadDetail()
        {
            var client = new RestClient(LinkAPI() + "/api/account/getdetailuser?strUser=" + GetUserId())
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + Token());
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode.ToString().Contains("Unauthorized"))
            {
                httpContext.HttpContext.Response.Redirect("/Account/AccessDenied");
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<IndexViewModel>(response.Content);
            }
        }
        public string GetRoleName()
        {
            var claims = httpContext.HttpContext.User.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.Role.ToString())
                ;

            return claims != null ? claims.Value : "";
        }
        public string GetUserId()
        {
            var claims = httpContext.HttpContext.User.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.Sid.ToString())
                ;
            return claims != null ? claims.Value : "";
        }
        public string GetUserName()
        {
            var claims = httpContext.HttpContext.User.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.Name.ToString())
                ;
            return claims != null ? claims.Value : "";
        }
        private string LinkAPI() => configuration.GetSection("LINK_API").Value;
        private string Token() {
            var claims = httpContext.HttpContext.User.Claims
                    .FirstOrDefault(p => p.Type == "Token")
                    ;

            return claims != null ? claims.Value : "";
        }
        //private JwtSecurityToken GetInfo(string strToken)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(strToken);
        //    return (JwtSecurityToken)jsonToken;
        //}
    }
}
