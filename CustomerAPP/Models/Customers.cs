﻿using System.ComponentModel.DataAnnotations;

namespace CustomerAPP.Models
{
    public class CreateCustomers
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
        //public string? LoginUser { get; set; }
        public string? Password { get; set; }
        //public bool IsActive { get; set; }
        public DateTime? Updated { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? IsActive { get; set; }

    }
}
