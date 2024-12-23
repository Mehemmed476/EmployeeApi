using EmployeeApp.DAL.Contexts;
using EmployeeApp.DAL.Helpers;
using EmployeeApp.DAL.Repositories.Abstractions;
using EmployeeApp.DAL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeApp.DAL.Extentions;

public static class ServicesExtension
{
    public static void AddDALServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            opt =>
            {
                opt.UseSqlServer(ConnectionStr.GetConnectionString());
            }
        );

        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    }
}