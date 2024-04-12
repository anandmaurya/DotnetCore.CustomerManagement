using Customer.Management.API.Models.Customer;
using CustomerManagement.Infrastrucure.Helper;
using CustomerManagement.Infrastrucure.Interface.Customer;
using CustomerManagement.Infrastrucure.Models.Common;
using CustomerManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Management.INFRASTRUCTRE.Services.Customer
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ApplicationDbContext _context;

        public CustomerServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customers>> GetCustomers()
        {
            try
            {
                var activeCustomers = await _context.Customers
                                                     .Where(c => (bool)c.IsActive == true)
                                                     .ToListAsync();

                return activeCustomers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ServiceResult<Customers>> GetCustomer(int id)
        {
            var customers = await _context.Customers.FindAsync(id);

            if (customers == null)
            {

                //return NotFound();
                return ServiceResult<Customers>.CreateFail("Customer Not Found.");
            }

            return ServiceResult<Customers>.CreateSuccess(customers, "Customer Fetch successfully.");
        }

        public async Task<Response> PostCustomers(Customers customer)
        {
            Response res = new Response();
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customer.Email);

            if (existingCustomer != null)
            {
                res.Result = false;
                res.Message = "Customer with this email already exists.";
                return res;
            }
            else
            {
                if (existingCustomer == null)
                {
                    customer.Password = EncryptionHelper.Encrypt(customer.Password);
                    customer.IsActive = true;
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();

                    // Successfully added the customer
                    res.Result = true;
                    res.Message = "Customer added successfully.";
                    return res;
                }
                else
                {
                    res.Result = false;
                    res.Message = "Customer are Not Created .";
                    return res;
                }

            }
        }

        public async Task<Response> PutCustomers(int id, Customers customers)
        {
            Response res = new Response();
            try
            {

                // read from db it the customer is exist or not
                if (id != customers.Id)
                {
                    res.Result = false;
                    res.Message = "Customer not exist.";
                    return res;
                }

                customers.IsActive = false;
                customers.Updated = DateTime.UtcNow;
                customers.UpdatedBy = 1;

                _context.Entry(customers).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                res.Result = false;
                res.Message = e.Message;
                return res;
            }

            res.Result = true;
            res.Message = "Customer updated successfully.";
            return res;

        }

        public async Task<Response> DeleteCustomers(int id)
        {
            Response res = new Response();
            try
            {
                var customerToDelete = await _context.Customers.FindAsync(id);

                if (customerToDelete == null)
                {
                    res.Result = false;
                    res.Message = "Customer not found.";
                    return res;
                }

                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();

                res.Result = true;
                res.Message = "Customer deleted successfully.";
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
