using Backend.Application.Interfaces.Repositories;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Repositories
{
    public class EmployeeRepository: GenericRepository<Employee>,IEmployeeRepository
    {

        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(string? search,string? sortBy,bool ascending)
        {
            var query =  _context.Employees.AsQueryable();

            //Search
            if(!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.Name.Contains(search) || e.Email.Contains(search));
            } 
            //sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch(sortBy.ToLower())
                {
                    case "name":
                        query = ascending ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name);
                        break;
                    //case "email":
                    //    query = ascending ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email);
                    //    break;
                    case "salary":
                        query = ascending ? query.OrderBy(e => e.Salary) : query.OrderByDescending(e => e.Salary);
                        break;
                    //default:
                    //    break;
                }
               
            }
            return await query.ToListAsync();
        }
    }
}
