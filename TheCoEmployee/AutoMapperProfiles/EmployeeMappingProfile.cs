using AutoMapper;

using Entities.DTOs;
using Entities.Models;

using System;

namespace CodeMazeApp.AutoMapperProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<EmployeeForUpdateDTO, Employee>().ReverseMap();
        }
    }
}
