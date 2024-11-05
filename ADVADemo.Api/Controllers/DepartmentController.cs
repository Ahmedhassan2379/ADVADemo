using ADVADemo.Application.Dtos.Department;
using ADVADemo.Application.Dtos.Employee;
using ADVADemo.Application.Interfaces;
using ADVADemo.Application.Specifications.DepartmentSpecification;
using ADVADemo.Application.Specifications.EmployeeSpecification;
using ADVADemo.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADVADemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IBaseRepository<Department> _baseRepository;
        private readonly IMapper _mapper;
        public DepartmentController(IBaseRepository<Department> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        [HttpGet("AllDepartments")]
        public async Task<ActionResult<List<DepartmentDto>>> GetAllDepartments()
        {
            var spec = new DepartmentWithManager();
            var departs = await _baseRepository.GetAllAsyncWithSpec(spec);
            var mapDeparts = _mapper.Map<List<DepartmentDto>>(departs);
            return Ok(mapDeparts);
        }

        [HttpGet("GetDepartment")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var spec = new DepartmentWithManager(id);
            var depart = await _baseRepository.GetByIdWithSpec(spec);
            var mapDepart = _mapper.Map<DepartmentDto>(depart);
            return Ok(mapDepart);
        }

        [HttpPost("NewDepartment")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto newdepartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var department = _mapper.Map<Department>(newdepartment);
            var spec = new DepartmentWithManager(newdepartment.ManagerId);
            var checkDepart =await _baseRepository.GetByIdWithSpec(spec);
            if(checkDepart == null)
            {
                await _baseRepository.AddAsync(department);
                return Ok(new { message = "Added Successfully" });
            }
            return BadRequest("Somethimg Error");
        }

        [HttpPut("EditeDepartment")]
        public async Task<ActionResult<Employee>> UpdateDepartment(CreateDepartmentDto newdepartment, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var spec = new DepartmentWithManager(id);
            var oldDepartment = await _baseRepository.GetByIdWithSpec(spec);
            if (oldDepartment == null)
            {
                return NotFound("This Department Not Found");
            }
            var mapDepartment = _mapper.Map<CreateDepartmentDto, Department>(newdepartment, oldDepartment);
            await _baseRepository.UpdateAsync(mapDepartment);
            return Ok(new {message= "Updated Successfully"});
        }
        [HttpDelete("RemoveDepartment")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var spec = new DepartmentWithManager(id);
            var existedDepartment = await _baseRepository.GetByIdWithSpec(spec);
            if (existedDepartment == null)
            {
                return NotFound("This Department Not Found");
            }
            await _baseRepository.DeleteAsync(existedDepartment);
            return Ok(new {message="Deleted Successfully"});
        }
    }
}
