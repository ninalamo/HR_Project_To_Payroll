using application.interfaces;
using domain;
using Microsoft.EntityFrameworkCore;
using persistence.infrastructure.extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AuditTrail> History { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSchedule> Schedules { get; set; }
        public DbSet<BioLog> RawLogs { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ConsolidatedBioLog> ConsolidatedTimeSheets { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Approver> Approvers { get; set; }
        public DbSet<RequestApprover> RequestApprovers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Shift>().HasData(new[] { 
                new Shift { Start = 700, End = 1600, IsActive = true, ID = 1},
                new Shift { Start = 800, End = 1700, IsActive = true, ID = 2},
                new Shift { Start = 830, End = 1730, IsActive = true, ID = 3},
                new Shift { Start = 900, End = 1800, IsActive = true, ID = 4},
            });

            Guid id = Guid.Parse("00000000-0000-0000-0000-000000000001");

            modelBuilder.Entity<Employee>().HasData(new[] {
                new Employee
                {
                    FirstName = "Sabin Alessa",
                    LastName = "Alamo",
                    ID = id,
                    CompanyEmail = "sabin.alessa@outlook.com",
                    PersonalEmail = "sabin.alessa@gmail.com",
                    EmployeeNumber = "112717",
                    IsActive = true,
                }
            });

            modelBuilder.Entity<EmployeeSchedule>().HasData(new[]
            {
                new EmployeeSchedule
                {
                    EmployeeID = id,
                    ShiftID = 3,
                    IsActive = true,
                    ID = 1
                }
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.ApplyTimeStamp();
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
