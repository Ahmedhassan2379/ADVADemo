using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Dtos.Department
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; }
        public int ManagerId { get; set; }
    }
}
