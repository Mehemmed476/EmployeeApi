using AutoMapper;
using EmployeeApp.BL.DTOs.AppUserDTOs;
using EmployeeApp.Core.Entities;

namespace EmployeeApp.BL.Profiles.AuthProfiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<LoginDto, AppUser>().ReverseMap()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        
        CreateMap<RegisterDto, AppUser>().ReverseMap()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.CheckPassword, opt => opt.Ignore());
        
        CreateMap<AppUserReadDto, AppUser>().ReverseMap();
    }
}