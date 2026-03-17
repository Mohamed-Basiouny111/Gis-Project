using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Gis_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int? RegionId { get; set; }
        public virtual Region? Region { get; set; }

        public bool? IsBlocked { get; set; } = false;


        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual ICollection<Payment> CreatedPayment { get; set; } = new List<Payment>();
        public virtual ICollection<Payment> UpdatedPayment { get; set; } = new List<Payment>();

        public virtual ICollection<Contract> CreatedContract { get; set; } = new List<Contract>();
        public virtual ICollection<Contract> UpdatedContract { get; set; } = new List<Contract>();

        public virtual ICollection<Asset> CreatedAsset { get; set; } = new List<Asset>();
        public virtual ICollection<Asset> UpdatedAsset { get; set; } = new List<Asset>();

        public virtual ICollection<Region> CreatedRegion { get; set; } = new List<Region>();
        public virtual ICollection<Region> UpdatedRegion { get; set; } = new List<Region>();

    }
}
