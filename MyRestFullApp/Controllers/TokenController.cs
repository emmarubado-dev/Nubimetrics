using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyRestFullApp.Authentication;
using MyRestFullApp.Core.Dtos;
using MyRestFullApp.Core.Entities;
using MyRestFullApp.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestFullApp.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private ITokenProvider _tokenProvider;
        private readonly ITokenServices _logic;
        private readonly ILogger<TokenController> _log;
        private readonly IConfiguration _configuration;


        public TokenController(ILogger<TokenController> log, ITokenServices logic, ITokenProvider tokenProvider, IConfiguration configuration)
        {
            this._log = log;
            this._logic = logic;
            this._tokenProvider = tokenProvider;
            this._configuration = configuration;
        }


        [HttpPost]
        public async Task<JsonWebToken> Post([FromBody] UsuarioDto userLogin)
        {
            var user = await _logic.ValidateUser(userLogin.UserName, userLogin.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var token = GenerateToken(user);

            //Se actualiza el token
            await _logic.RefreshToken(user.UserName, token.RefreshToken, DateTime.Now.AddMinutes(token.RefreshTokenExpired_in));

            return token;
        }


        [HttpPost]
        [Route("Refresh")]
        public async Task<JsonWebToken> Refresh([FromBody] RefreshAccessToken refreshAccessToken)
        {
            var principal = _tokenProvider.GetPrincipalFromExpiredToken(refreshAccessToken.AccessToken);
            var username = principal.Identity.Name;
            var claims = principal.Claims;

            var userId = claims.Where(x => x.Type.Contains("primarysid")).FirstOrDefault().Value;

            var user = await _logic.ValidateUser(userId);

            if (user == null || user.RefreshToken != refreshAccessToken.RefreshToken || user.RefreshTokenExpiredTime <= DateTime.Now)
            {
                throw new UnauthorizedAccessException("Invalid Refresh Token or Expired");
            }
            var token = GenerateToken(user);

            //Actualizamos el token
            await _logic.RefreshToken(userId, token.RefreshToken, DateTime.Now.AddMinutes(token.RefreshTokenExpired_in));

            return token;
        }


        private JsonWebToken GenerateToken(Usuario user)
        {

            int minutes = 60;
            int refreshMinutes = 70;

            if (_configuration.GetSection("TokenConfig")["Minutes"] != null)
            {
                int.TryParse(_configuration.GetSection("TokenConfig")["Minutes"].ToString(), out minutes);
            }

            if (_configuration.GetSection("TokenConfig")["MinutesRefresh"] != null)
            {
                int.TryParse(_configuration.GetSection("TokenConfig")["MinutesRefresh"].ToString(), out refreshMinutes);
            }
            var token = new JsonWebToken
            {
                Access_Token = _tokenProvider.CreateToken(user, DateTime.UtcNow.AddMinutes(minutes)),
                Expired_in = minutes, //minutes
                RefreshToken = _tokenProvider.GenerateRefreshToken(),
                RefreshTokenExpired_in = refreshMinutes
            };

            return token;
        }
    }
}
