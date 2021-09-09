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
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger,
            IAccountRepository accountRepository
            )
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }
        public async Task<IActionResult> ChangePassword()
        {

            return View();
        }
        public async Task<IActionResult> Index()
        {
            return View(await _accountRepository.LoadDetail());
        }
        public IActionResult Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("User");
            Response.Redirect("/");
            return Json(1);
        }
        public async Task<IActionResult> List()
        {
            ViewBag.ListUser = await _accountRepository.LoadListUser();
            ViewBag.ListRole = await _accountRepository.LoadListRoles();
            ViewBag.UserId = !string.IsNullOrWhiteSpace(_accountRepository.GetUserId())
                ? int.Parse(_accountRepository.GetUserId())
                : 0;
            ViewBag.CurrentRole = _accountRepository.GetRoleName();
            return View();
        }
    }
}
