using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestKhoaExample.CustomModels;
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
        #region Fields
        private readonly IConfiguration configuration;
        #endregion

        #region Contrsuctor
        public AccountController(
            IConfiguration _configuration
            )
        {
            configuration = _configuration;
        }
        #endregion

        #region Views
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                IndexViewModel detailUser = await LoadDetail();
                if (detailUser == null)
                {
                    Response.Redirect("/Home/Error?err=Empty-List-User");
                    return Json(0);
                }
                else return View(detailUser);

            }
            catch (Exception ex)
            {
                Response.Redirect("/Home/Error?err=" + ex.Message.ToString());
                return Json(0);
                throw;
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> List()
        {
            try
            {
                string currentRoleName = GetRoleName();

                List<AccountModel> listUser = await LoadListUserAsync();
                List<Role> listRole = await LoadListRolesAsync();
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
                ViewBag.UserCode = GetUserId();
                return View();
            }
            catch (Exception ex)
            {
                Response.Redirect("/Home/Error?err=" + ex.Message.ToString());
                return Json(0);
                throw;
            }
        }
        #endregion

        #region APIs
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
                Response.Cookies.Delete("uToken");
                Response.Redirect("/");
                return Json(1);
            }
            catch (Exception ex)
            {
                Response.Redirect("/Home/Error?err=" + ex.Message.ToString());
                return Json(0);
                throw;
            }
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> ApiSave(SaveViewModel model)
        {
            try
            {
                RestClient client = new (LinkAPI() + "/api/account/save")
                {
                    Timeout = -1
                };
                RestRequest request = new (Method.POST)
                {
                    AlwaysMultipartFormData = true
                };

                string byteImage = "";
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files[0];
                    using (MemoryStream ms = new())
                    {
                        file.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();
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

                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode.ToString().Contains("Unauthorized"))
                {
                    return Json(401);
                }
                else
                {
                    int result = JsonConvert.DeserializeObject<int>(response.Content);
                    return Json(result);
                }
            }
            catch
            {
                return Json(0);
                throw;
            }
        }
        [Authorize]
        public async Task<IActionResult> ApiChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                RestClient client = new (LinkAPI() + "/api/account/changepassword")
                {
                    Timeout = -1
                };
                RestRequest request = new (Method.POST)
                {
                    AlwaysMultipartFormData = true
                };

                request.AddHeader("Authorization", "Bearer " + Token());

                request.AddParameter("OldPassword", model.OldPassword);
                request.AddParameter("NewPassword", model.NewPassword);
                request.AddParameter("ConfirmPassword", model.ConfirmPassword);
                //request.AddParameter("StatusMessage", model.StatusMessage);
                request.AddParameter("strUser", GetUserId());

                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode.ToString().Contains("Unauthorized"))
                {
                    return Json("Err: Bạn không có quyền truy cập");
                }
                else
                {
                    string result = JsonConvert.DeserializeObject<string>(response.Content);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                return Json("Err: Có lỗi xảy ra. " + ex.Message.ToString());
                throw;
            }
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> ApiRemove(int iUserId)
        {
            try
            {
                RestClient client = new(LinkAPI() + "/api/account/remove")
                {
                    Timeout = -1
                };
                RestRequest request = new(Method.POST)
                {
                    AlwaysMultipartFormData = true
                };

                request.AddHeader("Authorization", "Bearer " + Token());

                request.AddParameter("iUserId", iUserId);
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode.ToString().Contains("Unauthorized"))
                {
                    return Json("Error: Bạn không có quyền truy cập");
                }
                else
                {
                    string result = JsonConvert.DeserializeObject<string>(response.Content);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                return Json("Error: Có lỗi xảy ra. " + ex.Message.ToString());
                throw;
            }
        }
        public async Task<IActionResult> ApiLogin(LoginViewModel model)
        {
            try
            {
                RestClient client = new(LinkAPI() + "/api/account/login")
                {
                    Timeout = -1
                };
                RestRequest request = new(Method.POST)
                {
                    AlwaysMultipartFormData = true
                };
                request.AddParameter("username", model.UserName);
                request.AddParameter("password", model.Password);
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode.ToString().Contains("Unauthorized"))
                {
                    return Json(new AccountModel
                    {
                        ErrorCode = "Bạn không có quyền truy cập"
                    });
                }
                else
                {
                    AccountModel result = JsonConvert.DeserializeObject<AccountModel>(response.Content);

                    if (string.IsNullOrEmpty(result.ErrorCode))
                    {
                        CookieOptions option = new()
                        {
                            HttpOnly = false,
                            Expires = DateTime.Now.AddMinutes(5),
                            Path = "/",
                            SameSite = SameSiteMode.Strict,
                            Secure = true
                        };
                        
                        Response.Cookies.Append("uToken", result.Token, option);

                        List<Claim> claims = new()
                        {
                            new Claim(ClaimTypes.Name, result.UserName),
                            new Claim(ClaimTypes.Sid, result.UserCode),
                            //new Claim("FullName", result.FullName),
                            //new Claim("Token", result.Token),
                            new Claim(ClaimTypes.Role, result.Role),
                        };

                        ClaimsIdentity claimsIdentity = new(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme
                            );

                        AuthenticationProperties authProperties = new()
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
            catch (Exception ex)
            {
                return Json(new AccountModel { ErrorCode = ex.Message.ToString() });
                throw;
            }
        }
        #endregion

        #region Methods
        private string LinkAPI() => configuration.GetSection("LINK_API") != null ? configuration.GetSection("LINK_API").Value : "";
        private string Token() => Request.Cookies["uToken"] != null ? Request.Cookies["uToken"].ToString() : "";
        private string GetUserId()
        {
            try
            {
                Claim claims = User.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.Sid.ToString())
                ;
                return claims != null ? claims.Value : "";
            }
            catch
            {
                return "";
                throw;
            }
        }
        private string GetRoleName()
        {
            try
            {
                Claim claims = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role.ToString());
                return claims != null ? claims.Value : "";
            }
            catch { return ""; throw; }
        }
        private async Task<List<AccountModel>> LoadListUserAsync()
        {
            try
            {
                RestClient client = new(LinkAPI() + "/api/account/ListUser")
                {
                    Timeout = -1
                };
                RestRequest request = new(Method.GET);
                request.AddHeader("Authorization", "Bearer " + Token());
                request.AlwaysMultipartFormData = true;
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode.ToString().Contains("Unauthorized"))
                {
                    Response.Redirect("/Account/AccessDenied");
                    return null;
                }
                else
                {
                    if (response.Content.Contains("err:"))
                    {
                        Response.Redirect("/Home/Error?err=" + response.Content.Replace("err:", ""));
                        return null;
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<AccountModel>>(response.Content);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/Home/Error?err=" + ex.Message.ToString());
                return null;
                throw;
            }
        }
        private async Task<List<Role>> LoadListRolesAsync()
        {
            try
            {
                RestClient client = new(LinkAPI() + "/api/account/getroles")
                {
                    Timeout = -1
                };
                RestRequest request = new(Method.GET);
                request.AddHeader("Authorization", "Bearer " + Token());
                request.AlwaysMultipartFormData = true;
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode.ToString().Contains("Unauthorized"))
                {
                    Response.Redirect("/Account/AccessDenied");
                    return null;
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<Role>>(response.Content);
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("/Home/Error?err=" + ex.Message.ToString());
                return null;
                throw;
            }
        }
        private async Task<IndexViewModel> LoadDetail()
        {
            try
            {
                RestClient client = new(LinkAPI() + "/api/account/getdetailuser?strUser=" + GetUserId())
                {
                    Timeout = -1
                };
                RestRequest request = new(Method.GET);
                request.AddHeader("Authorization", "Bearer " + Token());
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode.ToString().Contains("Unauthorized"))
                {
                    Response.Redirect("/Account/AccessDenied");
                    return null;
                }
                else
                {
                    return JsonConvert.DeserializeObject<IndexViewModel>(response.Content);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/Home/Error?err=" +  ex.Message.ToString());
                return null;
                throw;
            }
        }
        #endregion
    }
}
