using System.Text.RegularExpressions;
using FluentValidation;

namespace EmployeeApp.BL.DTOs.AppUserDTOs;

public class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string CheckPassword { get; set; }
}

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty")
            .MaximumLength(30).WithMessage("First name cannot be more than 30 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty")
            .MaximumLength(30).WithMessage("Last name cannot be more than 30 characters");
        
        RuleFor(x => x.UserName)
            .NotEmpty().NotNull().WithMessage("Username cannot be empty")
            .MaximumLength(30).WithMessage("Username cannot be more than 30 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage("Email cannot be empty")
            .Must(e => BeValidEmailAddress(e)).WithMessage("Invalid email address");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().NotNull().WithMessage("PhoneNumber cannot be empty");
            //.Must(pn => BeValidPhoneNumber(pn)).WithMessage("Invalid phone number");
        
        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Password cannot be empty");
        
        RuleFor(x => x.CheckPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
    
    public bool BeValidPhoneNumber(string phoneNumber)
    {
        Regex regex = new Regex(@"^\+994(50|51|55|70|77)\d{7}$");
        Match match = regex.Match(phoneNumber);
        return match.Success;
    }
    
    public bool BeValidEmailAddress(string email)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success) { return true; }
        return false;
    }
}