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
        private readonly IConfiguration _config;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IWebHostEnvironment _environment;
        public AccountController(IConfiguration config,
            IAccountRepository accountRepository,
            ITokenRepository tokenRepository,
            IWebHostEnvironment environment
            )
        {
            _environment = environment;
            _tokenRepository = tokenRepository;
            _accountRepository = accountRepository;
            _config = config;
        }
        /// <summary>
        /// asd
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _accountRepository.GetListRoleAsync());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            return Ok(await _accountRepository.SignInAsync(model));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListUser()
        {
            var _RoleName = _tokenRepository.GetRoleNameFromToken(await getToken());
            var _UserList = await _accountRepository.LoadListAll();
            switch (_RoleName)
            {
                case "User":
                    return Ok("");
                case "Admin":
                    _UserList = _UserList.Where(p => p.Role.ToLower() == "user").ToList();
                    return Ok(_UserList);
                case "SuperAdmin":
                    return Ok(_UserList);
                default:
                    return Ok("");
            }

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDetailUser()
        {
            var _UserId = await getUserId();
            return Ok(await _accountRepository.GetDetailAsync(_UserId));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove([FromForm] int iUserId)
        {
            return Ok(await _accountRepository.RemoveAsync(iUserId));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordViewModel model)
        {
            var _UserId = await getUserId();

            var _CheckInfo = await _accountRepository.CheckInfoAsync(_UserId, model.OldPassword);
            if (_CheckInfo)
            {
                return Ok(await _accountRepository.ChangePasswordAsync(_UserId, model.NewPassword));
            }
            else
            {
                return Ok("Err: Mật khẩu cũ nhập không đúng.");
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save([FromForm] SaveViewModel _Model)
        {
            var _User = new AccountModel();
            var _UpHinhAvatar = Request.Form.Files["UpAvatar"];

            if (_UpHinhAvatar != null)
            {
                _Model.Avatar = _UpHinhAvatar.FileName;

                string _RootPath = _environment.WebRootPath;
                string uploadsFolder = Path.Combine(_RootPath, "images/");

                #region Upload hình avatar
                //UpHinhPC

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
                #endregion
            }

            if (_Model.Type == "_Create")
            {
                _User.AccountId = _Model.Id;
                _User.FullName = _Model.FullName;
                _User.UserName = _Model.UserName;
                _User.Email = _Model.Email;
                _User.Address = _Model.Address;
                _User.Phone = _Model.Phone;
                _User.Gender = _Model.Gender;
                _User.Status = _Model.Status;
                _User.Avatar = _Model.Avatar;
                _User.Password = _Model.Password;
            }
            else if (_Model.Type == "_Edit")
            {
                _User = (await _accountRepository.LoadListAll()).FirstOrDefault(p => p.AccountId == _Model.Id);
                
                _User.FullName = _Model.FullName;
                _User.UserName = _Model.Email;
                _User.Email = _Model.Email;
                _User.Address = _Model.Address;
                _User.Phone = _Model.Phone;
                _User.Gender = _Model.Gender;
                _User.Status = _Model.Status;
                if (!string.IsNullOrEmpty(_Model.Avatar))
                {
                    _User.Avatar = _Model.Avatar;
                }
            }
            return Ok(await _accountRepository.SaveAsync(_User, _Model.RoleId));
        }
        private async Task<int> getUserId()
        {
            return _tokenRepository.GetUserIdFromToken(await getToken());
        }
        private async Task<string> getToken()
        {
            return await HttpContext.GetTokenAsync("Bearer", "access_token");
        }

    }
}
