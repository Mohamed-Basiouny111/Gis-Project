namespace Gis_Project.Models
{
    public class Asset : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int RegionId { get; set; }
        public virtual Region? Region { get; set; }

        public int? ContractId { get; set; }
        public virtual Contract? Contract { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
