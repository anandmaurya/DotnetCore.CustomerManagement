using Microsoft.AspNetCore.Mvc;
using Customer.Management.API.Models.Customer;
using CustomerManagement.Models;
using CustomerManagement.Infrastrucure.Interface.Customer;

namespace Customer.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _customer;
        public CustomersController(ICustomerServices customer)
        {

            _customer = customer;
        }
        // GET: api/GetCustomers
        [HttpGet]
        [Route("GetCustomer")]

        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomer()
        {
            try
            {
                var customers = await _customer.GetCustomers();

                if (customers == null)
                {
                    return NotFound();
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }


        //// GET: api/GetCustomerById/5
        [HttpGet]
        [Route("GetCustomerById/{id}")]

        public async Task<ActionResult<Customers>> GetCustomerById(int id)
        {
            var result = await _customer.GetCustomer(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }


        // PUT: api/PutCustomers/5
        [HttpPut]
        [Route("UpdateCustomer/{id}")]

        public async Task<ActionResult<Response>> UpdateCustomer(int id, Customers customers)
        {
            return await _customer.PutCustomers(id, customers);
        }


        // POST: api/PostCustomers
        [HttpPost]
        [Route("CreateCustomer")]

        public async Task<ActionResult<Response>> CreateCustomer(Customers customers)
        {
            return await _customer.PostCustomers(customers);
        }


        // DELETE: api/DeleteCustomers/5
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public async Task<Response> DeleteCustomer(int id)
        {
            return await _customer.DeleteCustomers(id);
        }


    }


}
