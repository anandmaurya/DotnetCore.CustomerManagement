using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Infrastrucure.Models.Authentication.Login
{
    public class LoginResponse
    {
        public bool Result { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
