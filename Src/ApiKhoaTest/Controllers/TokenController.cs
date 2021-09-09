using ApiKhoaTest.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiKhoaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _config;
        public TokenController(IConfiguration config,
                    ITokenRepository tokenRepository
                    )
        {
            _tokenRepository = tokenRepository;
            _config = config;
        }
        [HttpGet]
        public IActionResult GetToken()
        {
            return Ok("");
        }
    }
}
