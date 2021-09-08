using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestKhoa.Models.Manage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestKhoa.IRepository;
using TestKhoa.Models;
using TestKhoa.Models.Account;
//using TestKhoa.Services;

namespace TestKhoa.Controllers
{
    public class ManageController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AccountModel> _userManager;
        private readonly SignInManager<AccountModel> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IWebHostEnvironment _environment;
        [TempData]
        public string StatusMessage { get; set; }
        public ManageController(ILogger<HomeController> logger,
            UserManager<AccountModel> userManager,
            SignInManager<AccountModel> signInManager,
            RoleManager<IdentityRole> roleManager,
            IAccountRepository accountRepository,
            IWebHostEnvironment environment
            )
        {
            _environment = environment;
            _roleManager = roleManager;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles ="SuperAdmin, Admin")]
        public async Task<IActionResult> ListUser()
        {
            // Clear the existing external cookie to ensure a clean login process
            var _ListUser = await _accountRepository.LoadListAll();

            ViewBag.ListUser = _ListUser;
            ViewBag.ListRole = _roleManager.Roles.ToList();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Đổi mật khẩu thành công.";

            return RedirectToAction(nameof(ChangePassword));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage,
                FullName = user.FullName,
                Address = user.Address,
                Gender = user.Gender,
                Status = user.Status,
                Avatar = user.Avatar
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var _UpHinhAvatar = Request.Form.Files["UpAvatar"];

            if (_UpHinhAvatar != null)
            {
                model.Avatar = _UpHinhAvatar.FileName;

                string _RootPath = _environment.WebRootPath;
                string uploadsFolder = Path.Combine(_RootPath, "images/");

                #region Upload hình avatar
                //UpHinhPC

                if (_UpHinhAvatar != null)
                {
                    if (!string.IsNullOrEmpty(_UpHinhAvatar.FileName))
                    {
                        string pathfile = Path.Combine(uploadsFolder, _UpHinhAvatar.FileName);

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        using (var fileStream = new FileStream(pathfile, FileMode.Create))
                        {
                            _UpHinhAvatar.CopyTo(fileStream);
                        }
                    }
                }
                #endregion
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.FullName = model.FullName;
            //user.UserName = model.Email;
            user.Email = model.Email;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = model.Gender;
            //user.Status = model.Status;
            if (!string.IsNullOrEmpty(model.Avatar))
            {
                user.Avatar = model.Avatar;
            }
            var _Result = await _userManager.UpdateAsync(user);

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string strId)
        {
            var _User = _userManager.Users.FirstOrDefault(p => p.Id == strId);
            await _userManager.DeleteAsync(_User);

            return Json("OK");
        }
        public async Task<IActionResult> Save(SaveViewModel _Model)
        {
            var _UpHinhAvatar = Request.Form.Files["UpAvatar"];

            if (_UpHinhAvatar != null)
            {
                _Model.Avatar = _UpHinhAvatar.FileName;

                string _RootPath = _environment.WebRootPath;
                string uploadsFolder = Path.Combine(_RootPath, "images/");

                #region Upload hình avatar
                //UpHinhPC

                if (_UpHinhAvatar != null)
                {
                    if (!string.IsNullOrEmpty(_UpHinhAvatar.FileName))
                    {
                        string pathfile = Path.Combine(uploadsFolder, _UpHinhAvatar.FileName);

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        using (var fileStream = new FileStream(pathfile, FileMode.Create))
                        {
                            _UpHinhAvatar.CopyTo(fileStream);
                        }
                    }
                }
                #endregion
            }

            if (_Model.Type == "_Create")
            {
                var user = new AccountModel
                {
                    FullName = _Model.FullName,
                    UserName = _Model.Email,
                    Email = _Model.Email,
                    Address = _Model.Address,
                    PhoneNumber = _Model.Phone,
                    Gender = _Model.Gender,
                    Status = _Model.Status,
                    Avatar = _Model.Avatar
                };

                var result = await _userManager.CreateAsync(user, _Model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, _Model.Role);
                }
            }
            else if (_Model.Type == "_Edit")
            {
                var _User = _userManager.Users.FirstOrDefault(p => p.Id == _Model.Id);
                        _User.FullName = _Model.FullName;
                        _User.UserName = _Model.Email;
                        _User.Email = _Model.Email;
                        _User.Address = _Model.Address;
                        _User.PhoneNumber = _Model.Phone;
                        _User.Gender = _Model.Gender;
                        _User.Status = _Model.Status;
                if (!string.IsNullOrEmpty(_Model.Avatar))
                {
                    _User.Avatar = _Model.Avatar;
                }
                
                var _Result = await _userManager.UpdateAsync(_User);
                if (_Result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(_Model.Password))
                    {
                        var changePasswordResult = await _userManager.AddPasswordAsync(_User, _Model.Password);
                    }

                    await _userManager.RemoveFromRoleAsync(_User, (await _userManager.GetRolesAsync(_User))[0]);
                    await _userManager.AddToRoleAsync(_User, _Model.Role);
                }
            }
            return RedirectToAction(nameof(ManageController.ListUser), "Manage");
        }
        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
