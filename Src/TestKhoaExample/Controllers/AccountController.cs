using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestKhoaExample.CustomModels;
using TestKhoaExample.IRepository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TestKhoaExample.Models;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TestKhoaExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly IConfiguration configuration;
        public AccountController(
            IAccountRepository _accountRepository,
            IConfiguration _configuration
            )
        {
            configuration = _configuration;
            accountRepository = _accountRepository;
        }
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await accountRepository.LoadDetail());
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Redirect("/");
            return Json(1);
        }
        [Authorize(Roles ="SuperAdmin,Admin")]
        public async Task<IActionResult> List()
        {
            var currentRoleName = accountRepository.GetRoleName();
            if (!accountRepository.IsSigIn()
                || currentRoleName.ToLower() == "user"
                )
            {
                Response.Redirect("/");
                return Json("");
            }

            var listUser = await accountRepository.LoadListUser();
            var listRole = await accountRepository.LoadListRoles();
            switch (currentRoleName)
            {
                case "SuperAdmin":
                    break;
                case "Admin":
                    listRole = listRole.Where(p => p.RoleName == "User").ToList();
                    listUser = listUser.Where(p => p.Role == "User").ToList();
                    break;
                case "User":
                    listRole = new List<Role>();
                    listUser = new List<AccountModel>();
                    break;
                default:
                    listRole = new List<Role>();
                    listUser = new List<AccountModel>();
                    break;
            }

            ViewBag.ListUser = listUser;
            ViewBag.ListRoles = listRole;
            ViewBag.UserCode = accountRepository.GetUserId();
            ViewBag.CurrentRole = accountRepository.GetRoleName();
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult ApiSave(SaveViewModel model)
        {
            var client = new RestClient(LinkAPI() + "/api/account/save")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true
            };

            string byteImage = "";
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    byteImage = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }

            request.AddHeader("Authorization", "Bearer " + Token());

            request.AddParameter("Type", model.Type);
            request.AddParameter("Id", model.Id);
            request.AddParameter("FullName", model.FullName);
            request.AddParameter("UserName", model.UserName);
            request.AddParameter("Email", model.Email);
            request.AddParameter("Address", model.Address);
            request.AddParameter("Phone", model.Phone);
            request.AddParameter("Gender", model.Gender);
            request.AddParameter("Status", model.Status);
            request.AddParameter("Password", model.Password);
            request.AddParameter("RoleId", model.RoleId);
            request.AddParameter("ByteImage", byteImage);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString().Contains("Unauthorized"))
            {
                return Json(401);
            }
            else
            {
                var result = JsonConvert.DeserializeObject<int>(response.Content);
                return Json(result);
            }
        }
        public IActionResult ApiChangePassword(ChangePasswordViewModel model)
        {
            var client = new RestClient(LinkAPI() + "/api/account/changepassword")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true
            };

            request.AddHeader("Authorization", "Bearer " + Token());

            request.AddParameter("OldPassword", model.OldPassword);
            request.AddParameter("NewPassword", model.NewPassword);
            request.AddParameter("ConfirmPassword", model.ConfirmPassword);
            request.AddParameter("StatusMessage", model.StatusMessage);
            request.AddParameter("strUser", GetUserId());

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString().Contains("Unauthorized"))
            {
                return Json("Error: Bạn không có quyền truy cập");
            }
            else
            {
                var result = JsonConvert.DeserializeObject<string>(response.Content);
                return Json(result);
            }
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult ApiRemove(int iUserId)
        {
            var client = new RestClient(LinkAPI() + "/api/account/remove")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true
            };

            request.AddHeader("Authorization", "Bearer " + Token());

            request.AddParameter("iUserId", iUserId);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString().Contains("Unauthorized"))
            {
                return Json("Error: Bạn không có quyền truy cập");
            }
            else
            {
                var result = JsonConvert.DeserializeObject<string>(response.Content);
                return Json(result);
            }
        }
        public async Task<IActionResult> ApiLogin(LoginViewModel model)
        {
            var client = new RestClient(LinkAPI() + "/api/account/login")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true
            };
            request.AddParameter("username", model.UserName);
            request.AddParameter("password", model.Password);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString().Contains("Unauthorized"))
            {
                return Json(new AccountModel { 
                    ErrorCode = "Bạn không có quyền truy cập"
                });
            }
            else
            {
                var result = JsonConvert.DeserializeObject<AccountModel>(response.Content);

                if (string.IsNullOrEmpty(result.ErrorCode))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, result.UserName),
                        new Claim(ClaimTypes.Sid, result.UserCode),
                        //new Claim("FullName", result.FullName),
                        new Claim("Token", result.Token),
                        new Claim(ClaimTypes.Role, result.Role),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme
                        );

                    var authProperties = new AuthenticationProperties
                    {
                        //AllowRefresh = <bool>,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),
                        //IsPersistent = true,
                        //IssuedUtc = <DateTimeOffset>,
                        //RedirectUri = <string>
                    };

                    await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);
                }

                return Json(result);
            }
        }
        private string LinkAPI() => configuration.GetSection("LINK_API").Value;
        private string Token() {
            var claims = HttpContext.User.Claims
                    .FirstOrDefault(p => p.Type == "Token")
                    ;
            return claims != null ? claims.Value : "";
        }
        private string GetUserId()
        {
            var claims = HttpContext.User.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.Sid.ToString())
                ;
            return claims != null ? claims.Value : "";
        }
    }
}
