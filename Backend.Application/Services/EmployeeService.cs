using Backend.Application.DTOs.Employee;
using Backend.Application.Interfaces.Repositories;
using Backend.Application.Interfaces.Services;
using Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(int pageNumber, int pageSize, string? search,string? sortBy, bool ascending,decimal? minSalary,decimal? maxSalary)
        {
            var employees = (await _employeeRepository.GetAllAsync(pageNumber,pageSize,search,sortBy,ascending,minSalary,maxSalary));
            if(minSalary.HasValue)
            {
                employees = employees.Where(e => e.Salary >= minSalary.Value);
            }
            if(maxSalary.HasValue)
            {
                employees = employees.Where(e => e.Salary <= maxSalary.Value);
            }

            
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Salary = e.Salary

            });
        }
        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return null;
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary

            };
        }
        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            string? imagePath = null;
            if(dto.ProfileImage!=null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if(!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ProfileImage.FileName);
                var filePath=Path.Combine(uploadsFolder, fileName);
                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ProfileImage.CopyToAsync(stream);
                }
                imagePath= "/uploads/" + fileName;
            }
            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Salary = dto.Salary,
                Department = dto.Department,
                ProfileImage=imagePath

            };
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary

            };
        }
        public async Task UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                throw new Exception("Employee not found.");
            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Salary = dto.Salary;

            _employeeRepository.Update(employee);
            await
            _employeeRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var employee=await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                throw new Exception("Employee not found");
            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveChangesAsync();
        }
        }
    }

