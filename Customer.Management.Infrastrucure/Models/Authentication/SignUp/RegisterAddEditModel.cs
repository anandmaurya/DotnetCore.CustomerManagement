using System.ComponentModel.DataAnnotations;

namespace Customer.Management.API.Models.Authentication.SignUp
{
    public class RegisterAddEditModel
    {
        [Required(ErrorMessage = "FirstName is Required")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Required")]
        public string PhoneNumber { get; set; }

        public string LoginUser { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
