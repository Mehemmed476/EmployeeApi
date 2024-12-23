using AutoMapper;
using EmployeeApp.BL.DTOs.EmployeeDTOs;
using EmployeeApp.Core.Entities;

namespace EmployeeApp.BL.Profiles.EmployeeProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
        CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
        CreateMap<EmployeeReadDto, Employee>().ReverseMap();
    } 
}