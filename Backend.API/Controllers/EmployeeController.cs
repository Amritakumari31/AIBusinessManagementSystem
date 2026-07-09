using Backend.API.Responses;
using Backend.Application.DTOs.Employee;
using Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.API.Controllers
{
    [Authorize]
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
            return Ok(new ApiResponse<IEnumerable<EmployeeDto>>(true,"Employees fetched successfully",employees));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var employees = await _employeeService.GetByIdAsync(id);
            return Ok(new ApiResponse<EmployeeDto>(true, "Employees fetched successfully", employees));
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            var employee = await _employeeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = employee.Id },
                new ApiResponse<EmployeeDto>(true,"Employee created successfully",employee));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateEmployeeDto dto)

        {
            await _employeeService.UpdateAsync(id, dto);
            return Ok(new ApiResponse<string>(true,"Employee updated successfully",null));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)

        {
            await _employeeService.DeleteAsync(id);
            return Ok(new ApiResponse<string>(true,"Employee deleted successfully",null));
        }
    }
}
