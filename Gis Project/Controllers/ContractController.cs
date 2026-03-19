using Gis_Project.Models;
using Gis_Project.Repositories;
using Gis_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gis_Project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ContractController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContractController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var repo = _unitOfWork.GetRepository <Contract, int>();

            var model = new clsData_VM
            {
                Contracts = (await repo.GetAllAsync()).OrderByDescending(r => r.Id).ToList()
            };

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCreate(Contract contract)
        {
            if (contract == null)
            {
                TempData["NotifyError"] = "البيانات غير صالحة";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var repo = _unitOfWork.GetRepository<Contract, int>();

                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                contract.CreatedById = userId;

                await repo.AddAsync(contract);
                await _unitOfWork.CompleteAsync();

                TempData["NotifySuccess"] = "تم إضافة العنصر بنجاح";
                TempData["NotifyTitle"] = "عملية ناجحة.";
            }
            else
            {
                TempData["NotifyError"] = "تأكد من صحة البيانات المدخلة";
            }

            return RedirectToAction("Index");
        }

          [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var repo = _unitOfWork.GetRepository<Contract, int>();
            var contract = await repo.GetByIdAsync(id);

            if (contract == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction("Index");
            }

            return View("Update", contract);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Contract contract)
        {
            var contractRepo = _unitOfWork.GetRepository<Contract, int>();
            var paymentRepo = _unitOfWork.GetRepository<Payment, int>();

            var existingContract = await contractRepo.GetByIdAsync(contract.Id);

            if (existingContract == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
                return View("Update", contract);

            var hasPayments = (await paymentRepo.GetAllAsync())
                .Any(p => p.AssetId == existingContract.Asset.Id);

            existingContract.TenantName = contract.TenantName;

            if (!hasPayments)
            {
                existingContract.MonthlyInstallment = contract.MonthlyInstallment;
                existingContract.TotalInstallments = contract.TotalInstallments;
                existingContract.StartDate = contract.StartDate;
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            existingContract.UpdatedById = userId;
            existingContract.UpdatedAt = DateTime.UtcNow;

            contractRepo.Update(existingContract);
            await _unitOfWork.CompleteAsync();

            TempData["NotifySuccess"] = hasPayments
                ? "تم تعديل البيانات المسموح بها فقط لوجود مدفوعات"
                : "تم تعديل العنصر بنجاح";

            TempData["NotifyTitle"] = "عملية ناجحة";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contractRepo = _unitOfWork.GetRepository<Contract, int>();
            var paymentRepo = _unitOfWork.GetRepository<Payment, int>();
            var assetRepo = _unitOfWork.GetRepository<Asset, int>();

            var contract = await contractRepo.GetByIdAsync(id);

            if (contract == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction(nameof(Index));
            }

            var hasPayments = (await paymentRepo.GetAllAsync())
                                .Any(p => p.AssetId == contract?.Asset?.Id);

            if (hasPayments)
            {
                TempData["NotifyError"] = "لا يمكن حذف العقد لوجود مدفوعات مرتبطة به";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var asset = (await assetRepo.GetAllAsync())
                                .FirstOrDefault(a => a.ContractId == contract.Id);

                if (asset != null)
                {
                    asset.ContractId = null;
                    assetRepo.Update(asset);
                }

                contractRepo.Delete(contract);
                await _unitOfWork.CompleteAsync();

                TempData["NotifySuccess"] = "تم حذف العقد بنجاح";
                TempData["NotifyTitle"] = "عملية ناجحة";
            }
            catch (Exception)
            {
                TempData["NotifyError"] = "حدث خطأ أثناء الحذف";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
