using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestFullApp.Authentication
{
    public class RefreshAccessToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
