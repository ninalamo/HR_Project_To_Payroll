using domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace persistence.configuration
{
    public class Employee_Configuration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(i => i.ID).HasName("EmployeeID");
            builder.HasIndex(i => i.EmployeeNumber).IsUnique();
            builder.HasIndex(i => i.CompanyEmail).IsUnique();
            builder.Property(i => i.CompanyEmail).IsRequired();
            builder.Property(i => i.PersonalEmail).IsRequired();
            builder.HasIndex(i => new { i.FirstName, i.LastName });
        }
    }
}
