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

namespace ApiKhoaTest.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly ConnectDbContext context;
        public AccountRepository(
            ConnectDbContext _context
            )
        {
            context = _context;
        }
        public async Task<List<AccountModel>> LoadListAll()
        {
            var listUser = await (from p in context.Account
                           join q in context.AccountRole on p.AccountId equals q.AccountId
                           join r in context.Role on q.RoleId equals r.RoleId
                           select new AccountModel {
                               Address = p.Address,
                               Avatar = p.Avatar,
                               Email = p.Email,
                               FullName = p.FullName,
                               GenderName = (p.Gender != null && p.Gender == 1? "Nữ" : "Nam"),
                               Password = p.Password,
                               Phone = p.Phone,
                               Role = r.RoleName,
                               StatusName = (p.Status != null && p.Status == 1? "Đã kích hoạt": "Chưa kích hoạt"),
                               Status = p.Status,
                               UserName = p.UserName
                           }).ToListAsync();
            return listUser;
        }
        public async Task<List<Role>> GetListRoleAsync()
        {
            return await context.Role.ToListAsync();
        }
        public async Task<string> GetUserRoleAsync(int iUserId)
        {
            var role = await (from p in context.AccountRole
                         join q in context.Role on p.AccountRoleId equals q.RoleId
                         where p.AccountId == iUserId
                         select q).ToListAsync();
            return role.Count > 0 ? role[0].RoleName : "";
        }
        private string GetUserRole(int iUserId)
        {
            var role = (from p in context.AccountRole
                         join q in context.Role on p.RoleId equals q.RoleId
                         where p.AccountId == iUserId
                         select q).ToList();
            return role.Count > 0 ? role[0].RoleName : "";
        }
        public async Task<AccountModel> SignInAsync(LoginViewModel req)
        {
            var model = new AccountModel();
            string strUn = req.UserName.ToUpper();
            string strPW = req.Password;

            var result = await context.Account.Where(x => x.UserName.ToUpper() == strUn).SingleOrDefaultAsync();
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
                        Email = result.Email,
                        UserName = result.UserName,
                        AccountId = result.AccountId,
                        Role = GetUserRole(result.AccountId),
                        ErrorCode = ""
                    };
                    //model.Token = tokenRepository.GenerateToken(model);
                }
            }
            else
            {
                model = new AccountModel { ErrorCode = "Tài khoản này không tồn tại" };
            }
            return model;
        }
        public async Task<string> ChangePasswordAsync(int iUserId, string strNewPassword)
        {
            var userPassHash = Sha256(strNewPassword).ToUpper();
            var user = await context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserId);

            if (user != null && !string.IsNullOrEmpty(userPassHash))
            {
                user.Password = userPassHash;

                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return "Đổi mật khẩu thành công";
            }
            return "";
        }
        public async Task<IndexViewModel> GetDetailAsync(int iUserid)
        {
            var user = await context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserid);
            var model = new IndexViewModel
            {
                Id = user.AccountId,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.Phone,
                FullName = user.FullName,
                Address = user.Address,
                Gender = user.Gender.Value,
                Status = user.Status.Value,
                Avatar = user.Avatar
            };
            return model;
        }
        public async Task<string> RemoveAsync(int iUserid)
        {
            try
            {
                var user = await context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserid);

                if (user != null)
                {
                    context.Account.Remove(user);
                    await context.SaveChangesAsync();
                    return "";
                }
                return "User null";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();

            }

        }
        public async Task<bool> CheckInfoAsync(int iUserid, string strPassword)
        {
            try
            {
                string strPW;

                var user = await context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserid);

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

        }
        public async Task<int> SaveAsync(AccountModel user, int iIdRole)
        {
            try
            {
                int id = 0;
                if (user.AccountId > 0)
                {
                    var obj = await context.Account.Where(n => n.AccountId == user.AccountId).SingleOrDefaultAsync();
                    if (obj != null)
                    {
                        obj.FullName = user.FullName;
                        obj.UserName = user.Email;
                        obj.Email = user.Email;
                        obj.Address = user.Address;
                        obj.Phone = user.Phone;
                        obj.Gender = user.Gender;
                        obj.Status = user.Status;
                        obj.Avatar = user.Avatar;

                        context.Account.Update(obj);
                        await context.SaveChangesAsync();

                        id = obj.AccountId;
                    }
                }
                else
                {
                    string userPassHash = Sha256(user.Password);

                    var newUser = new Account
                    {
                        FullName = user.FullName,
                        UserName = user.Email,
                        Email = user.Email,
                        Address = user.Address,
                        Phone = user.Phone,
                        Gender = user.Gender,
                        Status = user.Status,
                        Avatar = user.Avatar,
                        Password = userPassHash
                    };

                    await context.Account.AddAsync(newUser);
                    await context.SaveChangesAsync();

                    id = (await context.Account.OrderByDescending(p => p.AccountId).FirstOrDefaultAsync()).AccountId;
                }

                var currentUserRole = context.AccountRole.Where(p => p.AccountId == id);
                if (currentUserRole.Count() > 0)
                {
                    context.AccountRole.RemoveRange(currentUserRole);
                    await context.SaveChangesAsync();
                }

                var newUserRole = new AccountRole { 
                    AccountId = id,
                    RoleId = iIdRole
                };
                await context.AddAsync(newUserRole);
                await context.SaveChangesAsync();

                return id;
            }
            catch
            {

                return 0;
            }
        }
        private string Sha256(string data)
        {
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                // Convert byte array to a string   
                var builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
