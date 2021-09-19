using ApiKhoaTest.CustomModels;
using ApiKhoaTest.IRepository;
using ApiKhoaTest.Models;
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
        #region Fields
        private readonly IAccountRepository accountRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly IWebHostEnvironment environment;
        #endregion

        #region Constructor
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
        #endregion

        /// <summary>
        /// asd
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                return Ok(await accountRepository.GetListRoleAsync());
            }
            catch
            {
                return Ok(new List<Role>());
                throw;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            try
            {
                AccountModel result = await accountRepository.SignInAsync(model);
                if (string.IsNullOrEmpty(result.ErrorCode))
                {
                    result.Token = tokenRepository.GenerateToken(result);
                }

                return Ok(result);
            }
            catch
            {
                return Ok(new AccountModel { ErrorCode = "Có lỗi xảy ra!" });
                throw;
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListUser()
        {
            try
            {
                return Ok(await accountRepository.LoadListAll());
            }
            catch (Exception ex)
            {
                return Ok("Err:" + ex.Message.ToString());
                throw;
            }

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDetailUser(string strUser)
        {
            try
            {
                return Ok(await accountRepository.GetDetailAsync(strUser));
            }
            catch
            {
                return Ok(new IndexViewModel());
                throw;
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove([FromForm] int iUserId)
        {
            try
            {
                return Ok(await accountRepository.RemoveAsync(iUserId));
            }
            catch
            {
                return Ok("Có lỗi xảy ra!");
                throw;
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordViewModel model)
        {
            try
            {
                var userCode = Request.Form["strUser"];

                bool checkInfo = await accountRepository.CheckInfoAsync(userCode, model.OldPassword);
                if (checkInfo)
                {
                    return Ok(await accountRepository.ChangePasswordAsync(userCode, model.NewPassword));
                }
                else
                {
                    return Ok("Err: Mật khẩu cũ nhập không đúng.");
                }
            }
            catch
            {
                return Ok("Err: Có lỗi xảy ra.");
                throw;
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save([FromForm] SaveViewModel model)
        {
            try
            {
                var upAvatar = Request.Form["ByteImage"];

                if (!string.IsNullOrEmpty(upAvatar))
                {
                    Image imageAvatar = LoadBase64(upAvatar);
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

                return Ok(await accountRepository.SaveAsync(model));
            }
            catch
            {
                return Ok(0);
                throw;
            }
        }
        //private async Task<string> GetToken()
        //{
        //    return await HttpContext.GetTokenAsync("Bearer", "accesstoken");
        //}
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
