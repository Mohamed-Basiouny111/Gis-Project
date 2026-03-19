using Gis_Project.Models;
using Gis_Project.Repositories;
using Gis_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gis_Project.Controllers
{
    [Authorize]
    public class AssetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles ="Collector,Admin")]
        public async Task<IActionResult> Index()
        {
            var assetRepo = _unitOfWork.GetRepository<Asset, int>();
            var userRepo = _unitOfWork.GetRepository<ApplicationUser, string>();

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepo.GetByIdAsync(userId);

            if (user == null)
            {
                TempData["NotifyError"] = "المستخدم غير موجود";
                return RedirectToAction("Login", "Account");
            }

            clsData_VM clsData_VM = new clsData_VM();

            if (User.IsInRole("Collector") && user.RegionId != null)
            {
                clsData_VM.Assets = (await assetRepo.GetAllAsync())
                            .Where(a => a.RegionId == user.RegionId)
                            .OrderByDescending(a => a.Id).ToList();
            }
            else if (User.IsInRole("Admin"))
            {
                clsData_VM.Assets = (await assetRepo.GetAllAsync())
                            .OrderByDescending(a => a.Id).ToList();
            }
            else
            {
                TempData["NotifyError"] = "ليس لديك صلاحية للوصول لهذه الصفحة";
                return RedirectToAction("Index", "Home");
            }

            return View(clsData_VM);
        }
       
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var regionRepo = _unitOfWork.GetRepository<Region, int>();
            var contractRepo = _unitOfWork.GetRepository<Contract, int>();

            var regions = await regionRepo.GetAllAsync();
            ViewBag.Regions = regions
                .OrderByDescending(r => r.Id)
                .Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                });

            var contracts = await contractRepo.GetAllAsync();
            ViewBag.Contracts = contracts
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.TenantName
                });

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Asset asset)
        {
            if (!ModelState.IsValid)
                return View(asset);

            var repo = _unitOfWork.GetRepository<Asset, int>();
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            asset.CreatedById = userId;

            repo.AddAsync(asset);
            await _unitOfWork.CompleteAsync();

            TempData["NotifySuccess"] = "تم إضافة الأصل بنجاح";
            TempData["NotifyTitle"] = "عملية ناجحة";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var assetRepo = _unitOfWork.GetRepository<Asset, int>();
            var regionRepo = _unitOfWork.GetRepository<Region, int>();
            var contractRepo = _unitOfWork.GetRepository<Contract, int>();

            var asset = await assetRepo.GetByIdAsync(id);
            if (asset == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction(nameof(Index));
            }

            var regions = await regionRepo.GetAllAsync();
            ViewBag.Regions = regions
                .Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = r.Id == asset.RegionId
                })
                .ToList();

            var contracts = await contractRepo.GetAllAsync();
            ViewBag.Contracts = contracts
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.TenantName,
                    Selected = asset.ContractId == c.Id
                })
                .ToList();

            return View(asset);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(Asset asset)
        {
            if (!ModelState.IsValid)
                return View(asset);

            var repo = _unitOfWork.GetRepository<Asset, int>();
            var existingAsset = await repo.GetByIdAsync(asset.Id);
            if (existingAsset == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction(nameof(Index));
            }

            existingAsset.Name = asset.Name;
            existingAsset.Address = asset.Address;
            existingAsset.Latitude = asset.Latitude;
            existingAsset.Longitude = asset.Longitude;
            existingAsset.RegionId = asset.RegionId;
            existingAsset.ContractId = asset.ContractId;

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            existingAsset.UpdatedById = userId;
            existingAsset.UpdatedAt = DateTime.UtcNow;

            repo.Update(existingAsset);
            await _unitOfWork.CompleteAsync();

            TempData["NotifySuccess"] = "تم تعديل الأصل بنجاح";
            TempData["NotifyTitle"] = "عملية ناجحة";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var assetRepo = _unitOfWork.GetRepository<Asset, int>();
            var paymentRepo = _unitOfWork.GetRepository<Payment, int>();
            var contractRepo = _unitOfWork.GetRepository<Contract, int>();

            var asset = await assetRepo.GetByIdAsync(id);
            if (asset == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction(nameof(Index));
            }

            var hasPayments = (await paymentRepo.GetAllAsync())
                                .Any(p => p.AssetId == asset.Id);

            if (hasPayments)
            {
                TempData["NotifyError"] = "لا يمكن حذف الأصل لوجود مدفوعات مرتبطة به";
                return RedirectToAction(nameof(Index));
            }

            if (asset.ContractId != null)
            {
                var contract = await contractRepo.GetByIdAsync(asset.ContractId.Value);
                if (contract != null)
                {
                    contract.Asset = null;
                    contractRepo.Update(contract);
                }
                asset.ContractId = null;
            }

            try
            {
                assetRepo.Delete(asset);
                await _unitOfWork.CompleteAsync();

                TempData["NotifySuccess"] = "تم حذف الأصل بنجاح";
                TempData["NotifyTitle"] = "عملية ناجحة";
            }
            catch (Exception)
            {
                TempData["NotifyError"] = "حدث خطأ أثناء الحذف";
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Collector,Admin")]
        public async Task<IActionResult> MapAsset(int id)
        {
            var repo = _unitOfWork.GetRepository<Asset, int>();
            var asset = await repo.GetByIdAsync(id);

            if (asset == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction("Index");
            }

            var assetLocation = new
            {
                asset.Id,
                asset.Name,
                asset.Address,
                asset.Latitude,
                asset.Longitude,
                TenantName = asset.Contract?.TenantName ?? "",
                Status = asset.Payments.Any(p => p.BillingMonth.Month == DateTime.Now.Month && p.PaidInstallment)
                            ? "تم التحصيل"
                            : "معلق"
            };

            return View(assetLocation);
        }
    }
}
