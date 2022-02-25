using MyRestFullApp.Core.Entities;
using System;
using System.Threading.Tasks;

namespace MyRestFullApp.Core.Interfaces.Services
{
    public interface  ITokenServices
    {
        Task<Usuario> ValidateUser(string userName, string password);
        Task<Usuario> ValidateUser(string userId);
        Task<bool> RefreshToken(string userId, string refreshToken, DateTime timeExpired);
    }
}
