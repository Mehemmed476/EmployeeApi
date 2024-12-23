using AutoMapper;
using EmployeeApp.BL.DTOs.DepartmentDTOs;
using EmployeeApp.Core.Entities;

namespace EmployeeApp.BL.Profiles.DepartmentProfiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentCreateDto, Department>().ReverseMap();
        CreateMap<DepartmentUpdateDto, Department>().ReverseMap();
        CreateMap<DepartmentReadDto, Department>().ReverseMap();
    }
}