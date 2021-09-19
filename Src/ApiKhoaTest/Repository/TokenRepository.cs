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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ApiKhoaTest.CustomModels;

namespace ApiKhoaTest.Repository
{
    public class TokenRepository: ITokenRepository
    {
        //public JwtSecurityToken GetInfo(string strToken)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(strToken);
        //    return (JwtSecurityToken)jsonToken;
        //}
        //public int GetUserIdFromToken(string strToken)
        //{
        //    string _result = "0";
        //    var _tokenInfo = GetInfo(strToken);

        //    var _payLoad = _tokenInfo.Payload.ToList();
        //    if (_payLoad.Count > 0)
        //    {
        //        foreach (var mPayLoad in _payLoad)
        //        {
        //            if (mPayLoad.Key.ToLower() == "userid")
        //            {
        //                _result = mPayLoad.Value.ToString();
        //            }
        //        }
        //    }

        //    return int.Parse(_result);
        //}
        //public string GetRoleNameFromToken(string strToken)
        //{
        //    string _result = "";
        //    var _tokenInfo = GetInfo(strToken);

        //    var _payLoad = _tokenInfo.Payload.ToList();
        //    if (_payLoad.Count > 0)
        //    {
        //        foreach (var mPayLoad in _payLoad)
        //        {
        //            if (mPayLoad.Key.ToLower() == "role")
        //            {
        //                _result = mPayLoad.Value.ToString();
        //            }
        //        }
        //    }

        //    return _result;
        //}
        public string GenerateToken(AccountModel account)
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            string issuer = "http://localhost";  //normally this will be your site URL    

            SymmetricSecurityKey securityKey = new (Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new (securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            List<Claim> permClaims = new();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", account.AccountId.ToString()));
            permClaims.Add(new Claim("name", account.UserName));
            permClaims.Add(new Claim("role", account.Role));

            //Create Security Token object by giving required parameters    
            JwtSecurityToken token = new (issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddMinutes(5),
                            signingCredentials: credentials);
            string jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }
    }
}
