using CustomerAPP.Helper;
using CustomerAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace CustomerAPP.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CustomerController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
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
            string apiURL = $"{_configuration["ApiSettings:APIBaseUrl"]}/api/Customers/CreateCustomer";

            var request = new HttpRequestMessage(HttpMethod.Post, apiURL)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Handle error response
            }


            return RedirectToAction("Customer/Index");
        }

        public IActionResult Edit()
        {
            return View();
        }

    }
}
