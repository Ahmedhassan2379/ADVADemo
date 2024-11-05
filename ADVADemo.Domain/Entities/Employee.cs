using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADVADemo.Domain.Common;

namespace ADVADemo.Domain.Entities
{
    public class Employee : BaseModel
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public ICollection<Employee> EmployeesUnderManger { get; set; }
        public Employee? Manager { get; set; }
        public int? ManagerId { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
