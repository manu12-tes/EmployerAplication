using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployerAplication.Models;

namespace EmployerAplication.Data
{
    public class EmployerAplicationContext : DbContext
    {
        public EmployerAplicationContext (DbContextOptions<EmployerAplicationContext> options)
            : base(options)
        {
        }

        public DbSet<EmployerAplication.Models.Employee> Employee { get; set; } = default!;
    }
}
