using domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace persistence.configuration
{
    public class Approver_Configuration : IEntityTypeConfiguration<Approver>
    {
        public void Configure(EntityTypeBuilder<Approver> builder)
        {
            builder.HasKey(i => i.ID).HasName("ApproverID");
            builder.HasIndex(i => new { i.ID, i.TypeOfRequest, i.Level });
        }
    }
}
