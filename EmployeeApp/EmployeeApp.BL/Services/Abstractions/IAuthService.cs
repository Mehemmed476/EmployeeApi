using EmployeeApp.BL.DTOs.AppUserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.BL.Services.Abstractions;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto registerDto, IUrlHelper baseUrl);
    Task<bool> LoginAsync(LoginDto loginDto);
    Task<bool> LogoutAsync();
    Task<bool> ConfirmEmailAsync(string userId, string token);
    Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, IUrlHelper baseUrl);
    Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    List<AppUserReadDto> GetAllUsers();
    AppUserReadDto GetUserById(string userId);
}