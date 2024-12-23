using System.Text.RegularExpressions;
using FluentValidation;

namespace EmployeeApp.BL.DTOs.EmployeeDTOs;

public class EmployeeCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int DepartmentId { get; set; }
}

public class EmployeeCreateDtoValidator : AbstractValidator<EmployeeCreateDto>
{
    public EmployeeCreateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().NotNull().WithMessage("First Name cannot be empty")
            .MaximumLength(30).WithMessage("First Name cannot be more than 50 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().NotNull().WithMessage("First Name cannot be empty")
            .MaximumLength(30).WithMessage("First Name cannot be more than 50 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage("Email cannot be empty")
            .Must(e => BeValidEmailAddress(e)).WithMessage("Invalid email address");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().NotNull().WithMessage("PhoneNumber cannot be empty")
            .Must(pn => BeValidPhoneNumber(pn)).WithMessage("Invalid phone number");

        RuleFor(x => x.Address)
            .NotEmpty().NotNull().WithMessage("Address cannot be empty");
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