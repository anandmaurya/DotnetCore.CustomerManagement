using Customer.Management.API.Models.Authentication.Login;
using Customer.Management.API.Models.Authentication.SignUp;
using CustomerManagement.Infrastrucure.Models.Authentication.Login;
using CustomerManagement.Infrastrucure.Models.Common;
using CustomerManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomerManagement.Infrastrucure.Services.Authentication.AuthenticationService;

namespace CustomerManagement.Infrastrucure.Interface.Authentication
{
    public interface IAuthenticationService
    {

        Task<Response> Registration(RegisterAddEditModel registerModel, string role);

        Task<ServiceResult<LoginResponse>> Login(LoginModel loginModel);
    }
}
