using System.Reflection;
using System.Text;
using EmployeeApp.BL.Extensions;
using EmployeeApp.BL.Profiles.AuthProfiles;
using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.BL.Services.Implementations;
using EmployeeApp.Core.Entities;
using EmployeeApp.DAL.Contexts;
using EmployeeApp.DAL.Extentions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    {
        opt.Password.RequiredLength = 8;
        opt.User.RequireUniqueEmail = true;
        opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        opt.Lockout.MaxFailedAccessAttempts = 4;
    }
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAutoMapper(typeof(AuthProfile));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddControllers();

builder.Services.AddAuthentication(cfg => {
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
}).AddJwtBearer(x => {

    x.TokenValidationParameters = new TokenValidationParameters
    {
        
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
                .GetBytes(builder.Configuration["Jwt:SecretKey"])
        ),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"]
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extension
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();