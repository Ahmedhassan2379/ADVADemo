using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVADemo.Application.Dtos.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string MangerName { get; set; }
        public int ManagerId { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public int Ccount { get; set; }
    }
}
