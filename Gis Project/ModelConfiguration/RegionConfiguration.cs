using Gis_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gis_Project.ModelConfiguration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(r => r.Name)
                  .IsRequired()
                  .HasMaxLength(150);

            // 1:N relation
            builder.HasMany(r => r.Assets)
              .WithOne(a => a.Region)
              .HasForeignKey(a => a.RegionId)
              .OnDelete(DeleteBehavior.Restrict);

            // 1:N relation
            builder.HasMany(r => r.Collectors)
                   .WithOne(c => c.Region)
                   .HasForeignKey(c => c.RegionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 1:N relation
            builder.HasOne(h => h.CreatedBy)
                .WithMany(u => u.CreatedRegion)
                .HasForeignKey(h => h.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.UpdatedBy)
                 .WithMany(u => u.UpdatedRegion)
                 .HasForeignKey(h => h.UpdatedById)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
