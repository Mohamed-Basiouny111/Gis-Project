namespace Gis_Project.Models
{
    public class Payment : BaseEntity
    {
        public int AssetId { get; set; }
        public virtual Asset? Asset { get; set; }

        public string CollectorId { get; set; } = null!;
        public virtual ApplicationUser Collector { get; set; } = null!;

        public decimal AmountPaid { get; set; }
        public bool PaidInstallment { get; set; }
        public decimal PaidLateFeesAmount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime BillingMonth { get; set; }
    }
}
