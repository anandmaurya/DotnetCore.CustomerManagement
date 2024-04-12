using System.ComponentModel.DataAnnotations;

namespace Customer.Management.API.Models.Customer
{
    public class Customers
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Required")]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        public string? Password { get; set; }

        public DateTime? Updated { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? IsActive { get; set; }


    }
}
