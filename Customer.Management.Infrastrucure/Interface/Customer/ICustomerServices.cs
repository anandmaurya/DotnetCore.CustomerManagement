using Customer.Management.API.Models.Customer;
using CustomerManagement.Infrastrucure.Models.Common;
using CustomerManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Infrastrucure.Interface.Customer
{
    public interface ICustomerServices
    {
        Task<IEnumerable<Customers>> GetCustomers();
        Task<ServiceResult<Customers>> GetCustomer(int id);
        Task<Response> PostCustomers(Customers customer);
        Task<Response> PutCustomers(int id, Customers customers);
        Task<Response> DeleteCustomers(int id);
    }
}
