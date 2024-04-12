using AutoMapper;
using Customer.Management.API.Mappings;
using Customer.Management.API.MIddleware;
using Customer.Management.API.Models.Authentication.SignUp;
using Customer.Management.INFRASTRUCTRE.Services.Customer;
using CustomerManagement.Infrastrucure.Interface.Authentication;
using CustomerManagement.Infrastrucure.Interface.Customer;
using CustomerManagement.Infrastrucure.Services.Authentication;
using CustomerManagement.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnect")));

builder.Services.AddSingleton<RequestResponseMiddleware>();

builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));

builder.Services.AddAutoMapper((cfg => cfg.AddProfile<MappingProfile>()));



var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestResponseMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
