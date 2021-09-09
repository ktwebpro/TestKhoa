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
        private readonly IHttpContextAccessor _httpContext;
        public AccountRepository(ILogger<AccountRepository> logger,
            IHttpContextAccessor httpContext
            )
        {
            _httpContext = httpContext;
        }
        public bool IsSigIn()
        {
            return _httpContext.HttpContext.Request.Cookies["User"] != null;
        }
        public async Task<List<Role>> LoadListRoles()
        {
            string _Token = _httpContext.HttpContext.Request.Cookies["User"];
            var client = new RestClient("https://localhost:44314/api/account/getroles")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + _Token);
            IRestResponse response = await client.ExecuteAsync(request);
            
            return JsonConvert.DeserializeObject<List<Role>>(response.Content);
        }
        public async Task<List<AccountModel>> LoadListUser()
        {
            string _Token = _httpContext.HttpContext.Request.Cookies["User"];
            var client = new RestClient("https://localhost:44314/api/account/ListUser")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + _Token);
            IRestResponse response = await client.ExecuteAsync(request);
            
            return JsonConvert.DeserializeObject<List<AccountModel>>(response.Content);
        }
        public async Task<IndexViewModel> LoadDetail()
        {
            string _Token = _httpContext.HttpContext.Request.Cookies["User"];
            var client = new RestClient("https://localhost:44314/api/account/getdetailuser")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + _Token);
            IRestResponse response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<IndexViewModel>(response.Content);
        }
        public string GetRoleName()
        {
            string _KQ = "";
            string strToken;

            if (IsSigIn())
            {
                strToken = _httpContext.HttpContext.Request.Cookies["User"];
                var _TokenInfo = GetInfo(strToken);

                var _PayLoad = _TokenInfo.Payload.ToList();
                if (_PayLoad.Count > 0)
                {
                    foreach (var mPayLoad in _PayLoad)
                    {
                        if (mPayLoad.Key.ToLower() == "role")
                        {
                            _KQ = mPayLoad.Value.ToString();
                        }
                    }
                }
            }
            return _KQ;
        }
        public string GetUserId()
        {
            string _KQ = "";
            string strToken;

            if (IsSigIn())
            {
                strToken = _httpContext.HttpContext.Request.Cookies["User"];
                var _TokenInfo = GetInfo(strToken);

                var _PayLoad = _TokenInfo.Payload.ToList();
                if (_PayLoad.Count > 0)
                {
                    foreach (var mPayLoad in _PayLoad)
                    {
                        if (mPayLoad.Key.ToLower() == "userid")
                        {
                            _KQ = mPayLoad.Value.ToString();
                        }
                    }
                }
            }
            return _KQ;
        }
        public string GetUserName()
        {
            string _KQ = "";
            string strToken;

            if (IsSigIn())
            {
                strToken = _httpContext.HttpContext.Request.Cookies["User"];
                var _TokenInfo = GetInfo(strToken);

                var _PayLoad = _TokenInfo.Payload.ToList();
                if (_PayLoad.Count > 0)
                {
                    foreach (var mPayLoad in _PayLoad)
                    {
                        if (mPayLoad.Key.ToLower() == "name")
                        {
                            _KQ = mPayLoad.Value.ToString();
                        }
                    }
                }
            }
            return _KQ;
        }
        private JwtSecurityToken GetInfo(string strToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(strToken);
            return (JwtSecurityToken)jsonToken;
        }
    }
}
