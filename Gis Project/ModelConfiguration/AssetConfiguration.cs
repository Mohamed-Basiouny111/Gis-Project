using Gis_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gis_Project.ModelConfiguration
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(hr => hr.Id);
            builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(200);
            builder.Property(a => a.Address)
                   .IsRequired()
                   .HasMaxLength(300);
            builder.Property(a => a.Latitude)
                   .IsRequired();
            builder.Property(a => a.Longitude)
                   .IsRequired();
           
            // 1:1 relation
            builder.HasOne(a => a.Contract)
                   .WithOne(c => c.Asset)
                   .HasForeignKey<Asset>(a => a.ContractId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 1:N relation
            builder.HasOne(h => h.CreatedBy)
                .WithMany(u => u.CreatedAsset)
                .HasForeignKey(h => h.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.UpdatedBy)
                 .WithMany(u => u.UpdatedAsset)
                 .HasForeignKey(h => h.UpdatedById)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
