using Backend.Application.Interfaces.Repositories;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Repositories
{
    public class EmployeeRepository: GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
