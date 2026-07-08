using Backend.Application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
        Task UpdateAsync(int id, UpdateEmployeeDto dto);
        Task DeleteAsync(int id);
    }
}
