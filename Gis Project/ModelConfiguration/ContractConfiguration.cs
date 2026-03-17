using Gis_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gis_Project.ModelConfiguration
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(c => c.TenantName)
               .IsRequired()
               .HasMaxLength(150);
            builder.Property(c => c.MonthlyInstallment)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.Property(c => c.StartDate)
                   .IsRequired();
            builder.Property(c => c.TotalInstallments)
                   .IsRequired();

            // 1:N relation
            builder.HasOne(h => h.CreatedBy)
                .WithMany(u => u.CreatedContract)
                .HasForeignKey(h => h.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.UpdatedBy)
                 .WithMany(u => u.UpdatedContract)
                 .HasForeignKey(h => h.UpdatedById)
                 .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
