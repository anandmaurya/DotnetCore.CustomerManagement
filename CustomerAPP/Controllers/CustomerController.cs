using CustomerAPP.Helper;
using CustomerAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace CustomerAPP.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomers customer)
        {
            if (!string.IsNullOrEmpty(customer.Email))
            {
                customer.Email = EncryptionHelper.Encrypt(customer.Email);
            }

            var jsonContent = JsonConvert.SerializeObject(customer);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7070/api/Customers/CreateCustomer")
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response if needed
                string responseData = await response.Content.ReadAsStringAsync();
                // Do something with responseData
                //return Ok(responseData);
            }
            else
            {
                // Handle error response
                //  return StatusCode((int)response.StatusCode);
            }


            return RedirectToAction("Customer/Index");
        }

        public IActionResult Edit()
        {
            return View();
        }

    }
}
