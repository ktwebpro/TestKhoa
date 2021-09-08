using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestKhoa.IRepository;
using TestKhoa.Models;

namespace TestKhoa.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly UserManager<AccountModel> _userManager;
        private readonly SignInManager<AccountModel> _signInManager;
        public AccountRepository(IServiceProvider service)
        {
            _userManager = service.GetRequiredService<UserManager<AccountModel>>();
            _signInManager = service.GetRequiredService<SignInManager<AccountModel>>();
        }
        public async Task<List<AccountModel>> LoadListAll()
        {
            var _ListUser = await _userManager.Users.ToListAsync();
            return _ListUser;
        }
    }
}
