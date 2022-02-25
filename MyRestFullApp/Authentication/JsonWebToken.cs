using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestFullApp.Authentication
{
    public class JsonWebToken
    {
        public string Access_Token { get; set; }
        public int Expired_in { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenExpired_in { get; set; }
    }
}
