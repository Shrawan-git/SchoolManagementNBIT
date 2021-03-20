using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Controllers;

namespace WebApplication.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }

        public DbSet<Student> tbl_Student { get; set; }

        public DbSet<Departments> tbl_Departments { get; set; }

        public static implicit operator StudentContext(StudentController v)
        {
            throw new NotImplementedException();
        }
    }
}
