using ADVADemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Specifications.DepartmentSpecification
{
    public class DepartmentWithManagerId : BaseSpecification<Department>
    {
        public DepartmentWithManagerId(int managerId):base(p=>p.ManagerId == managerId)
        {
            Includes.Add(p => p.Manager);
        }
    }
}
