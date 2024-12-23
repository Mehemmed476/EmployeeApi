using FluentValidation;

namespace EmployeeApp.BL.DTOs.AppUserDTOs;

public class LoginDto
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.UserNameOrEmail).NotEmpty().NotNull().WithMessage("Email or UserName is required.");
        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is required.");
    }
}