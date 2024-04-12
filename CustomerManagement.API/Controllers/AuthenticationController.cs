using Customer.Management.API.Models.Authentication.Login;
using Customer.Management.API.Models.Authentication.SignUp;
using CustomerManagement.Infrastrucure.Interface.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(RegisterAddEditModel registerModel, string role)
        {
            var result = await _service.Registration(registerModel, role);

            if (result.Result)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _service.Login(loginModel);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return Unauthorized(result.Message);
            }

        }


    }
}
