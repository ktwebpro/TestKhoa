using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using ApiKhoaTest.Models;
using System.IdentityModel.Tokens.Jwt;
using System;
using ApiKhoaTest.CustomModels;

namespace ApiKhoaTest.IRepository
{
    public interface ITokenRepository
    {
        //int GetUserIdFromToken(string strToken);
        //string GetRoleNameFromToken(string strToken);
        string GenerateToken(AccountModel _account);
        //JwtSecurityToken GetInfo(string strToken);
    }
}
