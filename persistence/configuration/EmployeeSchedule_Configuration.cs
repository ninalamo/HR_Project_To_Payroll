using domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace persistence.configuration
{
    public class EmployeeSchedule_Configuration : IEntityTypeConfiguration<EmployeeSchedule>
    {
        public void Configure(EntityTypeBuilder<EmployeeSchedule> builder)
        {
            builder.HasKey(i => i.ID).HasName("ScheduleID");
            builder.HasIndex(i => new { i.EmployeeID, i.ShiftID });
        }
    }
}
