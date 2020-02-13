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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            //base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.ApplyTimeStamp();
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
