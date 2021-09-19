using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApiKhoaTest.IRepository;
using TestKhoa.Data;
using Microsoft.Extensions.Logging;
using ApiKhoaTest.Models;
using ApiKhoaTest.CustomModels;
using Microsoft.AspNetCore.Http;

namespace ApiKhoaTest.Repository
{
    public class AccountRepository: IAccountRepository, IDisposable
    {
        private readonly ConnectDbContext context;
        private readonly IHttpContextAccessor httpContext;
        private bool disposedValue;

        public AccountRepository(
            ConnectDbContext _context,
            IHttpContextAccessor _httpContext
            )
        {
            httpContext = _httpContext;
            context = _context;
        }
        public async Task<List<AccountModel>> LoadListAll()
        {
            try
            {
                List<AccountModel> listUser = await (from p in context.Account
                                      join q in context.AccountRole on p.AccountId equals q.AccountId
                                      join r in context.Role on q.RoleId equals r.RoleId
                                      select new AccountModel
                                      {
                                          Address = p.Address,
                                          Avatar = !string.IsNullOrEmpty(p.Avatar)
                                          ? httpContext.HttpContext.Request.Scheme + "://" + httpContext.HttpContext.Request.Host + "/images/" + p.Avatar
                                          : "",
                                          Email = p.Email,
                                          FullName = p.FullName,
                                          GenderName = (p.Gender == 1 ? "Nữ" : "Nam"),
                                          Gender = p.Gender,
                                          Password = p.Password,
                                          Phone = p.Phone,
                                          Role = r.RoleName,
                                          StatusName = (p.Status == 1 ? "Đã kích hoạt" : "Chưa kích hoạt"),
                                          Status = p.Status,
                                          UserName = p.UserName,
                                          AccountId = p.AccountId,
                                          UserCode = p.UserCode
                                      }).ToListAsync();


                return listUser;
            }
            catch
            {
                return new List<AccountModel>();
                throw;
            }
            finally
            {
                Dispose(disposing: true);
            }
        }
        public async Task<List<Role>> GetListRoleAsync()
        {
            try
            {
                return await context.Role.ToListAsync();
            }
            catch
            {
                return new List<Role>();
                throw;
            }
            finally
            {
                Dispose(disposing: true);
            }
        }
        private string GetUserRole(int iUserId)
        {
            try
            {
                List<Role> role = (from p in context.AccountRole
                            join q in context.Role on p.RoleId equals q.RoleId
                            where p.AccountId == iUserId
                            select q).ToList();
                return role.Count > 0 ? role[0].RoleName : "";
            }
            catch
            {
                return "";
                throw;
            }
            finally
            {
                Dispose(disposing: true);
            }
        }
        public async Task<AccountModel> SignInAsync(LoginViewModel req)
        {
            try
            {
                AccountModel model;
                string strUn = req.UserName.ToUpper();
                string strPW = req.Password;

                Account result = await context.Account.Where(x => x.UserName.ToUpper() == strUn).SingleOrDefaultAsync();
                if (result != null)
                {
                    strPW = Sha256(strPW);

                    if (result.Password.ToUpper() != strPW.ToUpper())
                    {
                        model = new AccountModel { ErrorCode = "Tên đăng nhập hoặc mật khẩu không đúng!" };
                    }
                    else if (result.Status == 0)
                    {
                        model = new AccountModel { ErrorCode = "Tài khoản này chưa được kích hoạt" };
                    }
                    else
                    {
                        model = new AccountModel
                        {
                            UserCode = result.UserCode,
                            Email = result.Email,
                            UserName = result.UserName,
                            AccountId = result.AccountId,
                            Role = GetUserRole(result.AccountId),
                            ErrorCode = ""
                        };
                    }
                }
                else
                {
                    model = new AccountModel { ErrorCode = "Tài khoản này không tồn tại" };
                }
                return model;
            }
            catch (Exception ex)
            {
                return new AccountModel { ErrorCode = ex.Message.ToString() };
                throw;
            }
            finally
            {
                Dispose(disposing: true);
            }
        }
        public async Task<string> ChangePasswordAsync(string strUserCode, string strNewPassword)
        {
            try
            {
                string userPassHash = Sha256(strNewPassword).ToUpper();
                Account user = await context.Account.FirstOrDefaultAsync(p => p.UserCode == strUserCode);

                if (user != null && !string.IsNullOrEmpty(userPassHash))
                {
                    user.Password = userPassHash;

                    context.Entry(user).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    return "Đổi mật khẩu thành công";
                }
                return "";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message.ToString();
                throw;
            }
            finally
            {
                Dispose(disposing: true);
            }
        }
        public async Task<IndexViewModel> GetDetailAsync(string strUserCode)
        {
            try
            {
                Account user = await context.Account.FirstOrDefaultAsync(p => p.UserCode == strUserCode);
                IndexViewModel model = new ()
                {
                    Id = user.AccountId,
                    Username = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.Phone,
                    FullName = user.FullName,
                    Address = user.Address,
                    Gender = user.Gender,
                    Status = user.Status,
                    Avatar = user.Avatar
                };
                return model;
            }
            catch
            {
                return new IndexViewModel();
                throw;
            }
            finally
            {
                Dispose(disposing: true);
            }
        }
        public async Task<string> RemoveAsync(int iUserid)
        {
            try
            {
                Account user = await context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserid);

                if (user != null)
                {
                    context.Account.Remove(user);
                    await context.SaveChangesAsync();
                    return "";
                }
                return "User null";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                Dispose(disposing: true);
            }

        }
        public async Task<bool> CheckInfoAsync(string strUserCode, string strPassword)
        {
            try
            {
                string strPW;

                Account user = await context.Account.FirstOrDefaultAsync(p => p.UserCode == strUserCode);

                if (user != null)
                {
                    strPW = Sha256(strPassword);

                    return (user.Password.ToUpper() == strPW.ToUpper());
                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                Dispose(disposing: false);
            }

        }
        public async Task<int> SaveAsync(SaveViewModel model)
        {
            try
            {
                int id = 0;
                if (model.Type == "Create")
                {
                    string userPassHash = Sha256(model.Password);

                    Account newUser = new ()
                    {
                        FullName = model.FullName,
                        UserName = model.UserName,
                        Email = model.Email,
                        Address = model.Address,
                        Phone = model.Phone,
                        Gender = (byte) model.Gender,
                        Status = (sbyte) model.Status,
                        Avatar = model.Avatar,
                        Password = userPassHash,
                        //UserCode = GetRandomChar()
                        UserCode = Guid.NewGuid().ToString()
                    };

                    await context.Account.AddAsync(newUser);
                    await context.SaveChangesAsync();

                    id = (await context.Account.OrderByDescending(p => p.AccountId).FirstOrDefaultAsync()).AccountId;
                }
                else if (model.Type == "Edit")
                {
                    Account obj = await context.Account.Where(n => n.AccountId == model.Id).SingleOrDefaultAsync();
                    if (obj != null)
                    {
                        obj.FullName = model.FullName;
                        obj.UserName = model.UserName;
                        obj.Email = model.Email;
                        obj.Address = model.Address;
                        obj.Phone = model.Phone;
                        obj.Gender =(byte) model.Gender;
                        obj.Status =(sbyte) model.Status;
                        if (!string.IsNullOrEmpty(model.Avatar))
                        {
                            obj.Avatar = model.Avatar;
                        }

                        context.Account.Update(obj);
                        await context.SaveChangesAsync();

                        id = obj.AccountId;
                    }
                }

                IQueryable<AccountRole> currentUserRole = context.AccountRole.Where(p => p.AccountId == id);
                if (currentUserRole.Any())
                {
                    context.AccountRole.RemoveRange(currentUserRole);
                    await context.SaveChangesAsync();
                }

                AccountRole newUserRole = new ()
                {
                    AccountId = id,
                    RoleId = model.RoleId
                };
                await context.AddAsync(newUserRole);
                await context.SaveChangesAsync();

                return id;
            }
            catch
            {
                return 0;
            }
            finally
            {
                Dispose(disposing: true);
            }
        }
        private static string Sha256(string data)
        {
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                    // Convert byte array to a string   
                    StringBuilder builder = new ();
                    foreach (byte t in bytes)
                    {
                        builder.Append(t.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch
            {
                return "";
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        ~AccountRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
