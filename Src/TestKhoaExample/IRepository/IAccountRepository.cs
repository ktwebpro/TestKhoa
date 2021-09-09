using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using TestKhoaExample.CustomModels;
using TestKhoaExample.Models;

namespace TestKhoaExample.IRepository
{
    public interface IAccountRepository
    {
        Task<IndexViewModel> LoadDetail();
        string GetUserId();
        Task<List<Role>> LoadListRoles();
        Task<List<AccountModel>> LoadListUser();
        string GetUserName();
        bool IsSigIn(); string GetRoleName();
    }
}
