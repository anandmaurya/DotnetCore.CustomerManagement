using Customer.Management.API.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Management.INFRASTRUCTRE.Interface
{
    public interface ICustomerServices
    {
        Task<ActionResult<IEnumerable<Customers>>> GetCustomers();
        Task<ActionResult<Customers>> GetCustomers(int id);
        Task<IActionResult> PutCustomers(int id, Customers customers);
        Task<IActionResult> DeleteCustomers(int id);
    }
}
