namespace Gis_Project.Models
{
    public class Contract : BaseEntity
    {
        public string? TenantName { get; set; }
        public decimal MonthlyInstallment { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalInstallments { get; set; }
        public virtual Asset? Asset { get; set; }
    }


}
