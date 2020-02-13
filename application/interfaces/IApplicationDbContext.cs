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

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
