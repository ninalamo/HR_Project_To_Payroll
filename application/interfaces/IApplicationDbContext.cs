using domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace application.interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AuditTrail> History { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeSchedule> Schedules { get; set; }
        DbSet<BioLog> RawLogs { get; set; }
        DbSet<Shift> Shifts { get; set; }
        DbSet<ConsolidatedBioLog> ConsolidatedTimeSheets { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
