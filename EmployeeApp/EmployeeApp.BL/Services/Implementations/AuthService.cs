// Updated AuthService
using AutoMapper;
using EmployeeApp.BL.DTOs.AppUserDTOs;
using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.Core.Entities;
using EmployeeApp.DAL.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.BL.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IEmailService emailService)
    {
        _signInManager = signInManager;
        _mapper = mapper;
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto, IUrlHelper urlHelper)
    {
        var newUser = _mapper.Map<AppUser>(registerDto);
        
        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded) return false;

        _emailService.SendWelcomeEmail(newUser.Email);
        string userToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
        string? url = urlHelper.Action(
            action: "ConfirmEmail",
            controller: "Accounts",
            values: new { userId = newUser.Id, token = userToken },
            protocol: "https"
        );
        _emailService.SendConfirmEmail(newUser.Email, url);
        return true;
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, IUrlHelper urlHelper)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user == null)
            throw new EntityNotFoundException("User not found.");

        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        string? resetUrl = urlHelper.Action(
            action: "ResetPassword",
            controller: "Accounts",
            values: new { userId = user.Id, token = resetToken },
            protocol: "https"
        );
        _emailService.SendForgotPassword(forgotPasswordDto.Email, resetUrl);
        return true;
    }

    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        AppUser? searchedUser = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail) 
                               ?? await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);

        if (searchedUser == null)
            throw new EntityNotFoundException("User not found");

        var result = await _signInManager.PasswordSignInAsync(searchedUser, loginDto.Password, loginDto.RememberMe, true);
        return result.Succeeded;
    }

    public async Task<bool> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return true;
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string token)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId);
        if (user == null)
            throw new EntityNotFoundException("User not found.");
        
        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
        if (!result.Succeeded)
            throw new Exception("Password reset failed.");

        return true;
    }

    public List<AppUserReadDto> GetAllUsers()
    {
        var users = _userManager.Users.ToList();
        var usersDto = _mapper.Map<List<AppUserReadDto>>(users);

        return usersDto;
    }

    public AppUserReadDto GetUserById(string userId)
    {
        var user = _userManager.FindByIdAsync(userId);
        
        var userDto = _mapper.Map<AppUserReadDto>(user);
        
        return userDto;
    }
}