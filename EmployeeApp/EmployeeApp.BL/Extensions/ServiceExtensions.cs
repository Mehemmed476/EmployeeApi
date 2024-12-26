using EmployeeApp.BL.ExternalServices.Abstractions;
using EmployeeApp.BL.ExternalServices.Implementations;
using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.BL.Services.Implementations;
using EmployeeApp.DAL.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeApp.BL.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddDALServices();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddHttpContextAccessor();
        services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}