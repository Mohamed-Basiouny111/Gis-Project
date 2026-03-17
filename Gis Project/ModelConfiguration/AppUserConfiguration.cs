using Gis_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gis_Project.ModelConfiguration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(hr => hr.FirstName)
                .IsRequired().HasMaxLength(300);

            builder.Property(hr => hr.LastName)
                .IsRequired().HasMaxLength(300);
    }
}

}
