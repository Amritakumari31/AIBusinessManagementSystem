using Backend.Application.DTOs.Employee;
using Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
var employees=await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var employees = await _employeeService.GetByIdAsync(id);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            var employee = await _employeeService.CreateAsync(dto);
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateEmployeeDto dto)

        {
            await _employeeService.UpdateAsync(id, dto);
            return Ok("Employee updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)

        {
            await _employeeService.DeleteAsync(id);
            return Ok("Employee deleted successfully");
        }
    }
}
