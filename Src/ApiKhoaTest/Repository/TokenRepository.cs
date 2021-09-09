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
        private readonly ConnectDbContext _context;
        public TokenRepository(ILogger<TokenRepository> logger,
            ConnectDbContext Context
            )
        {
            _context = Context;
        }
        public JwtSecurityToken GetInfo(string strToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(strToken);
            return (JwtSecurityToken)jsonToken;
        }
        public int GetUserIdFromToken(string strToken)
        {
            string _KQ = "";
            var _TokenInfo = GetInfo(strToken);

            var _PayLoad = _TokenInfo.Payload.ToList();
            if (_PayLoad.Count > 0)
            {
                foreach (var mPayLoad in _PayLoad)
                {
                    if (mPayLoad.Key.ToLower() == "userid")
                    {
                        _KQ = mPayLoad.Value.ToString();
                    }
                }
            }

            return int.Parse(_KQ);
        }
        public string GetRoleNameFromToken(string strToken)
        {
            string _KQ = "";
            var _TokenInfo = GetInfo(strToken);

            var _PayLoad = _TokenInfo.Payload.ToList();
            if (_PayLoad.Count > 0)
            {
                foreach (var mPayLoad in _PayLoad)
                {
                    if (mPayLoad.Key.ToLower() == "role")
                    {
                        _KQ = mPayLoad.Value.ToString();
                    }
                }
            }

            return _KQ;
        }
        public string GenerateToken(AccountModel _Account)
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://localhost";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", _Account.AccountId.ToString()));
            permClaims.Add(new Claim("name", _Account.UserName));
            permClaims.Add(new Claim("role", _Account.Role));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }
    }
}
