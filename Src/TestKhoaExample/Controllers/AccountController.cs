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

namespace TestKhoaExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly IConfiguration config;
        public AccountController(
            IAccountRepository _accountRepository,
            IConfiguration _config
            )
        {
            config = _config;
            accountRepository = _accountRepository;
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            if (!accountRepository.IsSigIn())
            {
                Response.Redirect("/");
                return Json("");
            }
            return View(await accountRepository.LoadDetail());
        }
        public IActionResult Login()
        {
            if (accountRepository.IsSigIn())
            {
                Response.Redirect("/");
            }
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("User");
            Response.Cookies.Delete("uToken");
            Response.Redirect("/");
            return Json(1);
        }
        public async Task<IActionResult> List()
        {
            if (!accountRepository.IsSigIn()
                || accountRepository.GetRoleName().ToLower() == "user"
                )
            {
                Response.Redirect("/");
                return Json("");
            }

            ViewBag.ListUser = await accountRepository.LoadListUser();
            ViewBag.ListRoles = await accountRepository.LoadListRoles();
            ViewBag.UserCode = accountRepository.GetUserId();
            ViewBag.CurrentRole = accountRepository.GetRoleName();
            return View();
        }
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

            var result = JsonConvert.DeserializeObject<int>(response.Content);

            return Json(result);
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
            request.AddParameter("strUser", User());

            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<string>(response.Content);

            return Json(result);
        }
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

            var result = JsonConvert.DeserializeObject<string>(response.Content);

            return Json(result);
        }
        public IActionResult ApiLogin(LoginViewModel model)
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

            var result = JsonConvert.DeserializeObject<AccountModel>(response.Content);

            return Json(result);
        }
        private string LinkAPI() => config.GetSection("LINK_API").Value;
        private string Token() => Request.Cookies["uToken"];
        private string User() => Request.Cookies["User"];
    }
}
