using EmployeeApp.BL.DTOs.AppUserDTOs;
using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerDto, Url);
                if (!result)
                {
                    return BadRequest("Registration failed.");
                }

                return Ok("Registration successful.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                if (!result)
                {
                    return Unauthorized("Invalid credentials.");
                }

                return Ok("Login successful.");
            }
            catch (EntityNotFoundException)
            {
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.LogoutAsync();
                return Ok("Logout successful.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            try
            {
                var result = await _authService.ConfirmEmailAsync(userId, token);
                if (!result)
                {
                    return BadRequest("Email confirmation failed.");
                }

                return Ok("Email confirmation successful.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                var result = await _authService.ForgotPasswordAsync(forgotPasswordDto, Url);
                if (!result)
                {
                    return BadRequest("Password reset request failed.");
                }

                return Ok("Password reset request successful.");
            }
            catch (EntityNotFoundException)
            {
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var result = await _authService.ResetPasswordAsync(resetPasswordDto);
                if (!result)
                {
                    return BadRequest("Password reset failed.");
                }

                return Ok("Password reset successful.");
            }
            catch (EntityNotFoundException)
            {
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _authService.GetAllUsers();
                return Ok(users);
            }
            catch (EntityNotFoundException)
            {
                return NotFound("Users not found.");
            }
        }

        [HttpGet("GetUserById/{userId}")]
        public IActionResult GetUserById(string userId)
        {
            try
            {
                var user = _authService.GetUserById(userId);
                return Ok(user);
            }
            catch (EntityNotFoundException)
            {
                return NotFound("User not found.");
            }
        }
    }
}
