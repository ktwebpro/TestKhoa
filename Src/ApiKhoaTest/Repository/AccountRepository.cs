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
        private readonly ConnectDbContext _context;
        private readonly ITokenRepository _tokenRepository;
        public AccountRepository(ILogger<AccountRepository> logger,
            ConnectDbContext Context,
            ITokenRepository tokenRepository
            )
        {
            _tokenRepository = tokenRepository;
            _context = Context;
        }
        public async Task<List<AccountModel>> LoadListAll()
        {
            var _ListUser = await _context.Account.ToListAsync();
            var _ListResult = (from p in _ListUser
                               select new AccountModel
                               {
                                   AccountId = p.AccountId,
                                   Address = p.Address,
                                   Avatar = p.Avatar,
                                   Email = p.Email,
                                   FullName = p.FullName,
                                   Gender = p.Gender,
                                   Password = p.Password,
                                   Phone = p.Phone,
                                   Role = GetUserRole(p.AccountId),
                                   Status = p.Status,
                                   UserName = p.UserName
                               }).ToList();
            return _ListResult;
        }
        public async Task<List<Role>> GetListRoleAsync()
        {
            return await _context.Role.ToListAsync();
        }
        public async Task<string> GetUserRoleAsync(int iUserId)
        {
            var _Role = await (from p in _context.AccountRole
                         join q in _context.Role on p.AccountRoleId equals q.RoleId
                         where p.AccountId == iUserId
                         select q).ToListAsync();
            return _Role.Count > 0 ? _Role[0].RoleName : "";
        }
        private string GetUserRole(int iUserId)
        {
            var _Role = (from p in _context.AccountRole
                         join q in _context.Role on p.RoleId equals q.RoleId
                         where p.AccountId == iUserId
                         select q).ToList();
            return _Role.Count > 0 ? _Role[0].RoleName : "";
        }
        public async Task<AccountModel> SignInAsync(LoginViewModel req)
        {
            var model = new AccountModel();
            string strUn = req.UserName.ToUpper();
            string strPW = req.Password;

            var result = await _context.Account.Where(x => x.UserName.ToUpper() == strUn).SingleOrDefaultAsync();
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
                    model.Token = _tokenRepository.GenerateToken(model);
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
            var USER_PW = Sha256(strNewPassword).ToUpper();
            var _User = await _context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserId);

            if (_User != null && !string.IsNullOrEmpty(USER_PW))
            {
                _User.Password = USER_PW;

                _context.Entry(_User).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return "Đổi mật khẩu thành công";
            }
            return "";
        }
        public async Task<IndexViewModel> GetDetailAsync(int iUserid)
        {
            var _User = await _context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserid);
            var model = new IndexViewModel
            {
                Id = _User.AccountId,
                Username = _User.UserName,
                Email = _User.Email,
                PhoneNumber = _User.Phone,
                FullName = _User.FullName,
                Address = _User.Address,
                Gender = _User.Gender.Value,
                Status = _User.Status.Value,
                Avatar = _User.Avatar
            };
            return model;
        }
        public async Task<string> RemoveAsync(int iUserid)
        {
            try
            {
                var _User = await _context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserid);

                if (_User != null)
                {
                    _context.Account.Remove(_User);
                    await _context.SaveChangesAsync();
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

                var _User = await _context.Account.FirstOrDefaultAsync(p => p.AccountId == iUserid);

                if (_User != null)
                {
                    strPW = Sha256(strPassword);

                    return (_User.Password.ToUpper() == strPW.ToUpper());
                }
                return false;
            }
            catch
            {
                return false;

            }

        }
        public async Task<int> SaveAsync(AccountModel _User, int iIdRole)
        {
            try
            {
                int id = 0;
                if (_User.AccountId > 0)
                {
                    var obj = await _context.Account.Where(n => n.AccountId == _User.AccountId).SingleOrDefaultAsync();
                    if (obj != null)
                    {
                        obj.FullName = _User.FullName;
                        obj.UserName = _User.Email;
                        obj.Email = _User.Email;
                        obj.Address = _User.Address;
                        obj.Phone = _User.Phone;
                        obj.Gender = _User.Gender;
                        obj.Status = _User.Status;
                        obj.Avatar = _User.Avatar;

                        _context.Account.Update(obj);
                        await _context.SaveChangesAsync();

                        id = obj.AccountId;
                    }
                }
                else
                {
                    string USER_PW = Sha256(_User.Password);

                    var _NewUser = new Account
                    {
                        FullName = _User.FullName,
                        UserName = _User.Email,
                        Email = _User.Email,
                        Address = _User.Address,
                        Phone = _User.Phone,
                        Gender = _User.Gender,
                        Status = _User.Status,
                        Avatar = _User.Avatar,
                        Password = USER_PW
                    };

                    await _context.Account.AddAsync(_NewUser);
                    await _context.SaveChangesAsync();

                    id = (await _context.Account.OrderByDescending(p => p.AccountId).FirstOrDefaultAsync()).AccountId;
                }

                var _CurrentUserRole = _context.AccountRole.Where(p => p.AccountId == id);
                if (_CurrentUserRole.Count() > 0)
                {
                    _context.AccountRole.RemoveRange(_CurrentUserRole);
                    await _context.SaveChangesAsync();
                }

                var _NewUserRole = new AccountRole { 
                    AccountId = id,
                    RoleId = iIdRole
                };
                await _context.AddAsync(_NewUserRole);
                await _context.SaveChangesAsync();

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
