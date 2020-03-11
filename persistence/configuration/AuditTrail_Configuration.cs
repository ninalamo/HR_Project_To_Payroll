using domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace persistence.configuration
{
    public class AuditTrail_Configuration : IEntityTypeConfiguration<AuditTrail>
    {
       
        public void Configure(EntityTypeBuilder<AuditTrail> builder)
        {
            builder.HasKey(i => i.ID).HasName("AuditID");
        }
    }

    public class OverTimeRequest_Configuration : IEntityTypeConfiguration<OverTimeRequest>
    {
        public void Configure(EntityTypeBuilder<OverTimeRequest> builder)
        {
            builder.HasKey(i => i.ID).HasName("ID");
        }
    }
}
