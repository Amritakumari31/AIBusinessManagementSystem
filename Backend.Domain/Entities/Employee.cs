using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities
{
    public class Employee : BaseEntity 
    {
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary {  get; set; }
        public string Department { get; set; } = string.Empty;
        public string ? ProfileImage {  get; set; } 
    }
    }

