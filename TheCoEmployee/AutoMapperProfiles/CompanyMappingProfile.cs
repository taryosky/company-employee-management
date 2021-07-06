using AutoMapper;

using Entities.DTOs;
using Entities.Models;

using System;

namespace CodeMazeApp.AutoMapperProfiles
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyDTO>().ForMember(x => x.Address, opt => opt.MapFrom(c => string.Join(' ', c.Address, c.Country)));
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CompanyDTO, Company>();
            CreateMap<CompanyForCreationDto, Company>();
        }
    }
}
