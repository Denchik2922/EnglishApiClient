using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AuthResponse
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
    }
}
