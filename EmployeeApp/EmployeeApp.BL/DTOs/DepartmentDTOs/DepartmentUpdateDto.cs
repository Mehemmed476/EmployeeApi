using FluentValidation;

namespace EmployeeApp.BL.DTOs.DepartmentDTOs;

public class DepartmentUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class DepartmentUpdateDtoValidator : AbstractValidator<DepartmentUpdateDto>
{
    public DepartmentUpdateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().NotNull().WithMessage("Title is required.")
            .MaximumLength(40).WithMessage("Title must not exceed 50 characters.");
        
        RuleFor(x => x.Description)
            .NotEmpty().NotNull().WithMessage("Description is required.");
    }
}