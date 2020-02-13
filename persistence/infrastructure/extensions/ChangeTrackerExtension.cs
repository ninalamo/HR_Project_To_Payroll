using lib.common.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace persistence.infrastructure.extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void ApplyTimeStamp(this ChangeTracker changeTracker)
        {
            var now = DateTimeOffset.Now;

            foreach (var entry in changeTracker.Entries())
            {
                if (!(entry.Entity is ITimeStamp T)) continue;

                switch (entry.State)
                {
                    case EntityState.Modified:
                        T.ModifiedOn = now;
                        break;

                    case EntityState.Added:
                        T.CreatedOn = now;
                        T.ModifiedOn = now;
                        break;
                }
            }
        }
    }
}
