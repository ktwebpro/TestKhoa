using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using TestKhoa.Models;

namespace TestKhoa.IRepository
{
    public interface IAccountRepository
    {
        Task<List<AccountModel>> LoadListAll();
    }
}
