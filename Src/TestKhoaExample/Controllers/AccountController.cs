using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using TestKhoaExample.CustomModels;
using TestKhoaExample.IRepository;
using Microsoft.Extensions.Logging;

namespace TestKhoaExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        public AccountController(
            IAccountRepository _accountRepository
            )
        {
            accountRepository = _accountRepository;
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
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
            Response.Redirect("/");
            return Json(1);
        }
        public IActionResult List()
        {
            
            return View();
        }
    }
}
