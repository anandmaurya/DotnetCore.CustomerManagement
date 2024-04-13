using System.ComponentModel.DataAnnotations;

namespace CustomerAPP.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username name is Required")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Password  is Required")]
        public string? password { get; set; }
    }
}
