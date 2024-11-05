using ADVADemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Specifications.DepartmentSpecification
{
    public class DepartmentWithManager : BaseSpecification<Department>
    {
        public DepartmentWithManager()
        {
            Includes.Add(p => p.Manager);
        }
        public DepartmentWithManager(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.Manager);
        }
    }
}
