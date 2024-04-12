using System.ComponentModel.DataAnnotations;

namespace Customer.Management.API.Models.Authentication.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username name is Required")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Password  is Required")]
        public string? password { get; set; }
    }
}
