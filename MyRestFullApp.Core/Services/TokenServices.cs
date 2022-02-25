using MyRestFullApp.Core.Entities;
using MyRestFullApp.Core.Interfaces;
using MyRestFullApp.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRestFullApp.Core.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TokenServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Usuario> ValidateUser(string userName, string password)
        {
            var usuario = await _unitOfWork.Usuarios.GetById(userName);

            if (usuario != null)
            {
                if (usuario.Password == password)
                {
                    return usuario;
                }
            }
            return null;
        }

        public async Task<Usuario> ValidateUser(string userName)
        {
            return await _unitOfWork.Usuarios.GetById(userName);
        }

        public async Task<bool> RefreshToken(string userName, string refreshToken, DateTime timeExpired)
        {
            var usuario = await _unitOfWork.Usuarios.GetById(userName);
            if (usuario != null)
            {
                usuario.RefreshToken = refreshToken;
                usuario.RefreshTokenExpiredTime = timeExpired;

                _unitOfWork.Usuarios.Update(usuario);
                await _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
