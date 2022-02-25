using System;

namespace MyRestFullApp.Core.Entities
{
    public class Usuario
    {
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string Roles { get; set; }
        public DateTime RefreshTokenExpiredTime { get; set; }
    }
}
