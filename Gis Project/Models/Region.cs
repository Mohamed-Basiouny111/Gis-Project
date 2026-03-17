namespace Gis_Project.Models
{
    public class Region : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public virtual ICollection<ApplicationUser> Collectors { get; set; } = new List<ApplicationUser>();
    }
}
