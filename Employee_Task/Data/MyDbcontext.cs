using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee_Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Employee_Task.Data
{
    public class MyDbcontext:DbContext
    {
        public DbSet<Employee> User{ get; set; }
        public DbSet<Employee_task> Task{ get;set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Employee_Task;Integrated Security=True;Trust Server Certificate=True");
        }
        
    }
}
