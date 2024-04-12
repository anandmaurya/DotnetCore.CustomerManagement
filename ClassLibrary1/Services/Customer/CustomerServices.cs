using Customer.Management.API.Models.Customer;
using Customer.Management.INFRASTRUCTRE.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Management.INFRASTRUCTRE.Services.Customer
{
    public class CustomerServices : ICustomerServices
    {
        public Task<IActionResult> DeleteCustomers(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Customers>> GetCustomers(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> PutCustomers(int id, Customers customers)
        {
            throw new NotImplementedException();
        }
    }
}
