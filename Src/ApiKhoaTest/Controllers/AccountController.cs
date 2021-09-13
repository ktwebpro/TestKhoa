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
using System.Drawing;
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
        public async Task<IActionResult> GetRoles(string strUser)
        {
            var roleName = await accountRepository.GetUserRoleAsync(GetUserCode(strUser));
            var listRole = await accountRepository.GetListRoleAsync();
            switch (roleName)
            {
                case "User":
                    return Ok("");
                case "Admin":
                    listRole = listRole.Where(p => p.RoleName.ToLower() == "user").ToList();
                    return Ok(listRole);
                case "SuperAdmin":
                    return Ok(listRole);
                default:
                    return Ok("");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            var result = await accountRepository.SignInAsync(model);
            if (string.IsNullOrEmpty(result.ErrorCode))
            {
                result.Token = tokenRepository.GenerateToken(result);
            }

            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListUser(string strUser)
        {
            //admin@gmail.com|Admin|N4TWuQ
            var roleName = await accountRepository.GetUserRoleAsync(GetUserCode(strUser));
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
        public async Task<IActionResult> GetDetailUser(string strUser)
        {
            return Ok(await accountRepository.GetDetailAsync(GetUserCode(strUser)));
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
            var userCode = GetUserCode(Request.Form["strUser"]);

            var checkInfo = await accountRepository.CheckInfoAsync(userCode, model.OldPassword);
            if (checkInfo)
            {
                return Ok(await accountRepository.ChangePasswordAsync(userCode, model.NewPassword));
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
            
            var upAvatar = Request.Form["ByteImage"];

            if (!string.IsNullOrEmpty(upAvatar))
            {
                var imageAvatar = LoadBase64(upAvatar);
                string fileName = DateTime.Now.Millisecond.ToString() + ".jpg";
                model.Avatar = fileName;

                string RootPath = environment.WebRootPath;
                string uploadsFolder = Path.Combine(RootPath, "images/");

                #region Upload hình avatar
                //UpHinhPC

                if (!string.IsNullOrEmpty(fileName))
                {
                    string pathfile = Path.Combine(uploadsFolder, fileName);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    imageAvatar.Save(pathfile);
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
                user.Gender = (byte)model.Gender;
                user.Status = (sbyte)model.Status;
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
                user.Gender = (byte)model.Gender;
                user.Status = (sbyte)model.Status;
                if (!string.IsNullOrEmpty(model.Avatar))
                {
                    user.Avatar = model.Avatar;
                }
            }
            return Ok(await accountRepository.SaveAsync(user, model.RoleId));
        }
        private string GetUserCode(string strInfo)
        {
            return GetInfo(strInfo, 2);
        }
        //private async Task<string> GetToken()
        //{
        //    return await HttpContext.GetTokenAsync("Bearer", "accesstoken");
        //}
        private string GetRoleNameFromToken(string strInfo)
        {
            return GetInfo(strInfo, 1);
        }
        private string GetInfo(string strInfo, int iOrder)
        {
            if (string.IsNullOrEmpty(strInfo)
                    || !strInfo.Contains("|"))
                return "";
            else
            {
                string[] arrayUserInfo = strInfo.Split("|");
                if (arrayUserInfo.Length < 3)
                    return "";
                return arrayUserInfo[iOrder];
            }
        }
        public static Image LoadBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }

    }
}
