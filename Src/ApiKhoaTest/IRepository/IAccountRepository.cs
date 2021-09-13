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
        Task<IndexViewModel> GetDetailAsync(string strUserCode);
        Task<bool> CheckInfoAsync(string strUserCode, string strPassword);
        Task<string> ChangePasswordAsync(string strUserCode, string strNewPassword);
        Task<string> GetUserRoleAsync(string strUserCode);
        Task<AccountModel> SignInAsync(LoginViewModel req);
        Task<List<AccountModel>> LoadListAll();
        Task<int> SaveAsync(AccountModel user, int iIdRole);
    }
}
