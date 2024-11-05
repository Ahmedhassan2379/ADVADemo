using ADVADemo.Application.Dtos.Department;
using ADVADemo.Application.Dtos.Employee;
using ADVADemo.Domain.Entities;
using AutoMapper;

namespace ADVADemo.Api.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Employee Mapper
            CreateMap<Employee, EmployeeDto>()
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
           .ForMember(des => des.MangerName, opt => opt.MapFrom(src => src.Manager.Name));

            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<Department, DepartmentDto>()
                .ForMember(des => des.ManagerName, opt => opt.MapFrom(src => src.Manager.Name));
            #endregion

            #region Department Mapper
            CreateMap<CreateDepartmentDto, Department>();

            CreateMap<Department, CreateDepartmentDto>();
            #endregion
        }
    }
}
