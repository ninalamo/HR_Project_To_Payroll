using application.interfaces;
using domain;
using Microsoft.EntityFrameworkCore;
using persistence.infrastructure.extensions;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Shift>().HasData(new[] { 
                new Shift { Start = 700, End = 1600, IsActive = true, ID = 1},
                new Shift { Start = 800, End = 1700, IsActive = true, ID = 2},
                new Shift { Start = 830, End = 1730, IsActive = true, ID = 3},
                new Shift { Start = 900, End = 1800, IsActive = true, ID = 4},
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
