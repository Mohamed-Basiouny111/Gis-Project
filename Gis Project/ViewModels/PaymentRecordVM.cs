namespace Gis_Project.ViewModels
{
    public class PaymentRecordVM
    {
        public int PaymentId { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TenantName { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string CollectorName { get; set; } = string.Empty;
        public DateTime BillingMonth { get; set; }
        public decimal AmountPaid { get; set; }
        public bool PaidInstallment { get; set; }
        public decimal PaidLateFeesAmount { get; set; }
    }

}
