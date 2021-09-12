using ApiKhoaTest.CustomModels;
using ApiKhoaTest.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiKhoaTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly IWebHostEnvironment environment;
        public AccountController(
            IAccountRepository _accountRepository,
            ITokenRepository _tokenRepository,
            IWebHostEnvironment _environment
            )
        {
            environment = _environment;
            tokenRepository = _tokenRepository;
            accountRepository = _accountRepository;
        }
        /// <summary>
        /// asd
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await accountRepository.GetListRoleAsync());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            return Ok(await accountRepository.SignInAsync(model));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListUser()
        {
            var roleName = tokenRepository.GetRoleNameFromToken(await GetToken());
            var userList = await accountRepository.LoadListAll();
            switch (roleName)
            {
                case "User":
                    return Ok("");
                case "Admin":
                    userList = userList.Where(p => p.Role.ToLower() == "user").ToList();
                    return Ok(userList);
                case "SuperAdmin":
                    return Ok(userList);
                default:
                    return Ok("");
            }

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDetailUser()
        {
            var userId = await GetUserId();
            return Ok(await accountRepository.GetDetailAsync(userId));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove([FromForm] int iUserId)
        {
            return Ok(await accountRepository.RemoveAsync(iUserId));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordViewModel model)
        {
            var userId = await GetUserId();

            var checkInfo = await accountRepository.CheckInfoAsync(userId, model.OldPassword);
            if (checkInfo)
            {
                return Ok(await accountRepository.ChangePasswordAsync(userId, model.NewPassword));
            }
            else
            {
                return Ok("Err: Mật khẩu cũ nhập không đúng.");
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save([FromForm] SaveViewModel model)
        {
            var user = new AccountModel();
            var upAvatar = Request.Form.Files["UpAvatar"];

            if (upAvatar != null)
            {
                model.Avatar = upAvatar.FileName;

                string RootPath = environment.WebRootPath;
                string uploadsFolder = Path.Combine(RootPath, "images/");

                #region Upload hình avatar
                //UpHinhPC

                if (!string.IsNullOrEmpty(upAvatar.FileName))
                {
                    string pathfile = Path.Combine(uploadsFolder, upAvatar.FileName);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    using (var fileStream = new FileStream(pathfile, FileMode.Create))
                    {
                        upAvatar.CopyTo(fileStream);
                    }
                }
                #endregion
            }

            if (model.Type == "Create")
            {
                user.AccountId = model.Id;
                user.FullName = model.FullName;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Address = model.Address;
                user.Phone = model.Phone;
                user.Gender = model.Gender;
                user.Status = model.Status;
                user.Avatar = model.Avatar;
                user.Password = model.Password;
            }
            else if (model.Type == "Edit")
            {
                user = (await accountRepository.LoadListAll()).FirstOrDefault(p => p.AccountId == model.Id);

                user.FullName = model.FullName;
                user.UserName = model.Email;
                user.Email = model.Email;
                user.Address = model.Address;
                user.Phone = model.Phone;
                user.Gender = model.Gender;
                user.Status = model.Status;
                if (!string.IsNullOrEmpty(model.Avatar))
                {
                    user.Avatar = model.Avatar;
                }
            }
            return Ok(await accountRepository.SaveAsync(user, model.RoleId));
        }
        private async Task<int> GetUserId()
        {
            return tokenRepository.GetUserIdFromToken(await GetToken());
        }
        private async Task<string> GetToken()
        {
            return await HttpContext.GetTokenAsync("Bearer", "accesstoken");
        }

    }
}
