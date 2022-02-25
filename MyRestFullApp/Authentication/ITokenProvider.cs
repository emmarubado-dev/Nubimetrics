using Microsoft.IdentityModel.Tokens;
using MyRestFullApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyRestFullApp.Authentication
{
    public interface ITokenProvider
    {
        string CreateToken(Usuario user, DateTime expiry);
        TokenValidationParameters GetValidationParameters();
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
