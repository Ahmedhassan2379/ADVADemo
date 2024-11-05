using ADVADemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Specifications.EmployeeSpecification
{
    public class EmployeeByDepartIdWithDepartmentAndManager : BaseSpecification<Employee>
    {
        public EmployeeByDepartIdWithDepartmentAndManager(int departId) : base(p => p.DepartmentId == departId)
        {
            Includes.Add(p => p.Department);
            Includes.Add(p => p.Manager);
        }
    }
}
