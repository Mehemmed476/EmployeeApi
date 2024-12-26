using EmployeeApp.BL.DTOs.AppUserDTOs;
using EmployeeApp.Core.Entities;

namespace EmployeeApp.BL.ExternalServices.Abstractions;

public interface IJwtTokenService
{
    string GenerateJwtToken(AppUser user);
}