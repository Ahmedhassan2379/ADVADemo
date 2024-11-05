using ADVADemo.Application.Dtos.Department;
using ADVADemo.Application.Dtos.Employee;
using ADVADemo.Application.Interfaces;
using ADVADemo.Application.Specifications;
using ADVADemo.Application.Specifications.EmployeeSpecification;
using ADVADemo.Domain.Entities;
using ADVADemo.Domain.Entities.SpecEntities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ADVADemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IBaseRepository<Employee> _baseRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IBaseRepository<Employee> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        [HttpGet("AllEmployees")]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees([FromQuery]EmployeeSpecParams specParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var spec = new EmployeeWithManagerAndDepartment(specParams);
            var employees = await _baseRepository.GetAllAsyncWithSpec(spec);
            var mapEmployees = _mapper.Map<List<EmployeeDto>>(employees);
            var specCount = new EmployeeSpecParamsTotal(specParams);
            var count = await _baseRepository.GetCountAsync(specCount);
            return Ok(new {mapEmployees,count});
        }

        [HttpGet("GetEmployee")]
        public async Task<ActionResult<DepartmentDto>> GetEmployee(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var spec = new EmployeeWithManagerAndDepartment(id);
            var employee = await _baseRepository.GetByIdWithSpec(spec);
            var mapEmployee = _mapper.Map<EmployeeDto>(employee);
            return Ok(mapEmployee);
        }

        [HttpPost("NewEmployee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto newemployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employee = _mapper.Map<Employee>(newemployee);
            await _baseRepository.AddAsync(employee);
            return Ok(new {message="Added Successfully"});
        }

        [HttpPut("EditeEmployee")]
        public async Task<ActionResult<Employee>> UpdateEmployee(CreateEmployeeDto newemployee , int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var spec = new EmployeeWithManagerAndDepartment(id);
            var oldEmployee = await _baseRepository.GetByIdWithSpec(spec);
            if (oldEmployee == null)
            {
                return NotFound("This Employee Not Found");
            }
            var mapEmployee = _mapper.Map<CreateEmployeeDto, Employee>(newemployee, oldEmployee);
            await _baseRepository.UpdateAsync(mapEmployee);
            return Ok(new {message="Updated Successfully"});
        }
        [HttpDelete("RemoveEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var spec = new EmployeeWithManagerAndDepartment(id);
            var existedEmployee = await _baseRepository.GetByIdWithSpec(spec);
            if (existedEmployee == null)
            {
                return NotFound("This Employee Not Found");
            }
            await _baseRepository.DeleteAsync(existedEmployee);
            return Ok(new {message="Deleted Successfully"});
        }

        [HttpGet("GetEmployeesByDepartment/{departName}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesByDepartment(int departId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var spec = new EmployeeByDepartIdWithDepartmentAndManager(departId);
            var employees = await _baseRepository.GetByDepartIdWithSpec(spec);
            var mapEmployees = _mapper.Map<List<EmployeeDto>>(employees);
            return Ok(mapEmployees);
        }
    }
}
