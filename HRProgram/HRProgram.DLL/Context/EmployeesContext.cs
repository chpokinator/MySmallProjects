using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HRProgram.DLL.Context
{
    public partial class EmployeesContext : DbContext
    {
        public EmployeesContext()
            : base("name=EmployeesContext")
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Summary> Summary { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Premium)
                .HasPrecision(19, 4);
        }
    }
}
