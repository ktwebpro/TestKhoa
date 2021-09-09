using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using ApiKhoaTest.Models;
using ApiKhoaTest.CustomModels;

namespace ApiKhoaTest.IRepository
{
    public interface IAccountRepository
    {
        Task<List<Role>> GetListRoleAsync();
        Task<string> RemoveAsync(int iUserid);
        Task<IndexViewModel> GetDetailAsync(int iUserid);
        Task<bool> CheckInfoAsync(int iUserid, string strPassword);
        Task<string> ChangePasswordAsync(int iUserId, string strNewPassword);
        Task<string> GetUserRoleAsync(int iUserId);
        Task<AccountModel> SignInAsync(LoginViewModel req);
        Task<List<AccountModel>> LoadListAll();
        Task<int> SaveAsync(AccountModel _User, int iIdRole);
    }
}
