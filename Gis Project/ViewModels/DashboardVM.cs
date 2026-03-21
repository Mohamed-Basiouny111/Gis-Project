namespace Gis_Project.ViewModels
{
    public class DashboardVM
    {
        public int TotalAssets { get; set; }
        public decimal TotalLateAmount { get; set; }
        public decimal TodayCollection { get; set; }

        public List<CollectorPerformanceVM> Collectors { get; set; } = new();
        public List<AssetMapVM> Assets { get; set; } = new();
    }
}
