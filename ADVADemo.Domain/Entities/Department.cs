using ADVADemo.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ADVADemo.Domain.Entities
{
    public class Department : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Employee? Manager { get; set; }
        public int? ManagerId { get; set; }
    }
}
