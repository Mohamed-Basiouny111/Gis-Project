using Gis_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gis_Project.ModelConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.AmountPaid)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.Property(p => p.PaidLateFeesAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.Property(p => p.PaidInstallment)
                   .IsRequired();
            builder.Property(p => p.BillingMonth)
                   .IsRequired();

            builder.Property(p => p.Date)
             .IsRequired()
             .HasDefaultValueSql("GETUTCDATE()");

            // 1:N relation
            builder.HasOne(p => p.Asset)
               .WithMany(a => a.Payments)
               .HasForeignKey(p => p.AssetId)
               .OnDelete(DeleteBehavior.Cascade);

            // 1:N relation
            builder.HasOne(h => h.CreatedBy)
                .WithMany(u => u.CreatedPayment)
                .HasForeignKey(h => h.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.UpdatedBy)
                 .WithMany(u => u.UpdatedPayment)
                 .HasForeignKey(h => h.UpdatedById)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Collector)
             .WithMany(c => c.Payments)
             .HasForeignKey(p => p.CollectorId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
