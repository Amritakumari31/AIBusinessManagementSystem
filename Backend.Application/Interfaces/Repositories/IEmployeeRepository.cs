using Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllAsync(int pageNumber,int pageSize,string? search,string?sortBy,bool ascending, decimal? minSalary,decimal?maxSalary);
        
    }
}
