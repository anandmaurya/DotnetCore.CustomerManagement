using CustomerAPP.Helper;
using CustomerAPP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace CustomerAPP.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AccountController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Authenticate([FromBody] LoginModel loginModel)
        {
            try
            {
                LoginResponse authResult = new LoginResponse();
                var jsonContent = JsonConvert.SerializeObject(loginModel);
                string apiURL = $"{_configuration["ApiSettings:APIBaseUrl"]}/api/Authentication/Login";

                var request = new HttpRequestMessage(HttpMethod.Post, apiURL)
                {
                    Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseData);
                    authResult.APIToken = tokenResponse.token;
                    authResult.Result = tokenResponse.result ?? null;
                    authResult.UserEmail = tokenResponse.userEmail ?? null;
                    authResult.UserName = tokenResponse.userName ?? null;
                    authResult.Expiration = tokenResponse.expiration ?? null;
                    HttpContext.Session.SetString("APITokenDetails", authResult.APIToken);

                    #region Claim

                    // Add claims
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, authResult.UserName),
                            new Claim(ClaimTypes.Email, authResult.UserEmail),
                            new Claim("APIToken", authResult.APIToken),
                            new Claim("Expiration", authResult.Expiration.ToString())
                        };

                    // Create a new identity
                    var identity = new ClaimsIdentity(claims, "MyAuthScheme");

                    // Create a new authentication ticket
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, "MyAuthScheme");

                    // Sign in the user
                    await HttpContext.SignInAsync("MyAuthScheme", principal, new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15), // Set the expiration time
                        IsPersistent = false
                    });
                    #endregion

                    return new JsonResult(authResult);
                }
                else
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return new JsonResult(responseData);
                    //user not authenticated
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new JsonResult(null);
        }
    }
}
