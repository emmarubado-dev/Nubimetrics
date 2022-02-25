using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MyRestFullApp.Core.Entities;

namespace MyRestFullApp.Authentication
{
    public class JwtProvider : ITokenProvider
    {
        private RsaSecurityKey _key;
        private string _algoritm;
        private string _issuer;
        private string _audience;

        public JwtProvider(string issuer, string audience, string keyName)
        {
            var parameters = new CspParameters() { KeyContainerName = keyName };
            var provider = new RSACryptoServiceProvider(2048, parameters);

            _key = new RsaSecurityKey(provider);
            _algoritm = SecurityAlgorithms.RsaSha256Signature;
            _issuer = issuer;
            _audience = audience;
        }
        public string CreateToken(Usuario user, DateTime expiry)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{user.Nombre} {user.Apellido}"),
                new Claim(ClaimTypes.Role, user.Roles),
                new Claim(ClaimTypes.PrimarySid, user.UserName)
            }, "Custom"
           );

            var token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = _audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(_key, _algoritm),
                Expires = expiry.ToUniversalTime(),
                Subject = identity
            });

            return tokenHandler.WriteToken(token);

        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = _key,
                ValidAudience = _audience,
                ValidIssuer = _issuer,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0)
            };
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = GetValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
