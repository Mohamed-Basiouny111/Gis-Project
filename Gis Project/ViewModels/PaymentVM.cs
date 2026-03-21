using System.ComponentModel.DataAnnotations;

namespace Gis_Project.ViewModels
{
    public class PaymentVM
    {
        public int AssetId { get; set; }

        public string? AssetName { get; set; }
        public string? Address { get; set; }
        public string? TenantName { get; set; }

        public decimal MonthlyInstallment { get; set; }
        public decimal LateAmount { get; set; }

        public bool PaidInstallment { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "القيمة غير صحيحة")]
        public decimal PaidLateAmount { get; set; }

        public int TotalInstallments { get; set; }        
        public int RemainingInstallments { get; set; }
    }
}
