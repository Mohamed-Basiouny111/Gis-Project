using Gis_Project.Models;
using Gis_Project.Repositories;
using Gis_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gis_Project.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var assetRepo = _unitOfWork.GetRepository<Asset, int>();
            var paymentRepo = _unitOfWork.GetRepository<Payment, int>();
            var userRepo = _unitOfWork.GetRepository<ApplicationUser, string>();

            var assets = await assetRepo.GetAllAsync();
            var payments = await paymentRepo.GetAllAsync();
            var users = await userRepo.GetAllAsync();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = users.FirstOrDefault(u => u.Id == userId);

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin)
            {
                assets = assets.Where(a => a.RegionId == currentUser.RegionId);
                payments = payments.Where(p => p.CollectorId == userId);
            }

            var totalExpected = assets.Sum(a => a.Contract.MonthlyInstallment * a.Contract.TotalInstallments);
            var totalPaid = payments.Sum(p => p.AmountPaid);

            var vm = new DashboardVM
            {
                TotalAssets = assets.Count(),
                TotalLateAmount = totalExpected - totalPaid,
                TodayCollection = payments
                    .Where(p => p.Date.Date == DateTime.Today)
                    .Sum(p => p.AmountPaid),

                Assets = assets.Select(a => new AssetMapVM
                {
                    Name = a.Name,
                    Address = a.Address,
                    Lat = a.Latitude,
                    Lng = a.Longitude,
                    RegionName = a.Region.Name
                }).ToList(),

                Collectors = isAdmin
                    ? payments.GroupBy(p => p.CollectorId)
                        .Select(g => new CollectorPerformanceVM
                        {
                            Name = users.FirstOrDefault(u => u.Id == g.Key).FUllName,
                            TotalCollected = g.Sum(x => x.AmountPaid)
                        }).ToList()
                    : new List<CollectorPerformanceVM>()
            };

            return View(vm);
        }
    }
}
