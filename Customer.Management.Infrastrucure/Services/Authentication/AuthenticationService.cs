using AutoMapper;
using Customer.Management.API.Models.Authentication.Login;
using Customer.Management.API.Models.Authentication.SignUp;
using CustomerManagement.Infrastrucure.Interface.Authentication;
using CustomerManagement.Models.Authentication.SignUp;
using CustomerManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Infrastrucure.Models.Common;
using CustomerManagement.Infrastrucure.Models.Authentication.Login;
using CustomerManagement.Infrastrucure.Interface.Email;

namespace CustomerManagement.Infrastrucure.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _email;


        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, IEmailService email)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _email = email;
        }

        public async Task<ServiceResult<LoginResponse>> Login(LoginModel loginModel)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginModel.username);

                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.password))
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    var userRoles = await _userManager.GetRolesAsync(user);

                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwtToken = GetToken(authClaims);

                    var tokenResponse = new LoginResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        Expiration = jwtToken.ValidTo,
                        Result = true,
                        UserEmail = user.Email ?? "",
                        UserName = user.UserName
                    };

                    return ServiceResult<LoginResponse>.CreateSuccess(tokenResponse, "Login success");
                }
                else
                {
                    return ServiceResult<LoginResponse>.CreateFail("Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<LoginResponse>.CreateFail("An error occurred during authentication.");
            }
        }

        public async Task<Response> Registration(RegisterAddEditModel registerModel, string role)
        {
            try
            {
                var dto = _mapper.Map<Register>(registerModel);

                // Check Customer Exist
                var customerExist = await _userManager.FindByEmailAsync(dto.Email);
                if (customerExist != null)
                {
                    return new Response { Result = false, Message = "Customer Already Exists!" };
                }

                // Add the Customer in Database
                ApplicationUser user = new()
                {
                    Email = dto.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    UserName = dto.UserName,
                    PhoneNumber = dto.PhoneNumber,
                };

                await _email.SendEmail(user.Email, "Unlock Your Account: Password and Username Inside", "This is your Username = " + dto.UserName + " " + "Password = " + dto.Password + " Please do not share UaseNae and Password with any other person. Thank you for your understanding and cooperation.");

                if (await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _userManager.CreateAsync(user, dto.Password);
                    if (!result.Succeeded)
                    {
                        var errorMessage = result.Errors.FirstOrDefault()?.Description;
                        return new Response { Result = false, Message = errorMessage };
                    }
                    // Add role Customer
                    await _userManager.AddToRoleAsync(user, role);
                    return new Response { Result = true, Message = "Customer Created Successfully." };
                }
                else
                {
                    return new Response { Result = false, Message = "This Role Does Not Exist." };
                }
            }
            catch (Exception ex)
            {
                return new Response { Result = false, Message = ex.Message };
            }
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

    }
}
