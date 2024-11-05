using ADVADemo.Domain.Entities;
using ADVADemo.Domain.Entities.SpecEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Specifications.EmployeeSpecification
{
    public class EmployeeSpecParamsTotal : BaseSpecification<Employee>
    {
        public EmployeeSpecParamsTotal(EmployeeSpecParams spec)
            :base(p =>
                     (!spec.managerId.HasValue || p.ManagerId == spec.managerId.Value) &&
                     (!spec.departmentId.HasValue || p.DepartmentId == spec.departmentId.Value) &&
                     (string.IsNullOrEmpty(spec.Search) || p.Name.ToLower().Contains(spec.Search))
            )
        {
            
        }
    }
}
