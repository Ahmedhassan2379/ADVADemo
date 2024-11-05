using ADVADemo.Domain.Entities;
using ADVADemo.Domain.Entities.SpecEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Specifications.EmployeeSpecification
{
    public class EmployeeWithManagerAndDepartment : BaseSpecification<Employee>
    {
        public EmployeeWithManagerAndDepartment()
        {
            Includes.Add(p => p.Department);
            Includes.Add(p => p.Manager);
        }
        public EmployeeWithManagerAndDepartment(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.Department);
            Includes.Add(p => p.Manager);
        }

        public EmployeeWithManagerAndDepartment(EmployeeSpecParams spec)
            :base(p=>
                     (!spec.managerId.HasValue || p.ManagerId==spec.managerId.Value)&&
                     (!spec.departmentId.HasValue || p.DepartmentId == spec.departmentId.Value) &&
                     (string.IsNullOrEmpty(spec.Search)|| p.Name.ToLower().Contains(spec.Search))
            )
        {
            Includes.Add(p => p.Department);
            Includes.Add(p => p.Manager);
            ApplyPaging(spec.PageSize * (spec.pageNumber - 1), spec.PageSize);
        }

    }
}
