using Gis_Project.Models;
using Gis_Project.Repositories;
using Gis_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gis_Project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
{
    var paymentRepo = _unitOfWork.GetRepository<Payment, int>();
    var assetRepo = _unitOfWork.GetRepository<Asset, int>();

    var payments = await paymentRepo.GetAllAsync();
    var assets = await assetRepo.GetAllAsync();

    var model = payments.Select(p =>
    {
        var asset = assets.FirstOrDefault(a => a.Id == p.AssetId);

        return new PaymentRecordVM
        {
            PaymentId = p.Id,
            AssetName = asset?.Name ?? "-",
            Address = asset?.Address ?? "-",
            TenantName = asset?.Contract?.TenantName ?? "-",
            RegionName = asset?.Region?.Name ?? "-",
            CollectorName = p.Collector?.FUllName ?? "-",
            BillingMonth = p.BillingMonth,
            AmountPaid = p.AmountPaid,
            PaidInstallment = p.PaidInstallment,
            PaidLateFeesAmount = p.PaidLateFeesAmount
        };
    }).OrderByDescending(p => p.BillingMonth).ToList();

    return View(model);
}

        public async Task<IActionResult> Delete(int id)
        {
            var paymentRepo = _unitOfWork.GetRepository<Payment, int>();
            var assetRepo = _unitOfWork.GetRepository<Asset, int>();

            var payment = await paymentRepo.GetByIdAsync(id);
            if (payment == null)
            {
                TempData["Notifyinfo"] = "سجل الدفع غير موجود";
                return RedirectToAction("Index");
            }

            var asset = await assetRepo.GetByIdAsync(payment.AssetId);
            if (asset == null)
            {
                TempData["Notifyinfo"] = "الأصل المرتبط غير موجود";
                return RedirectToAction("Index");
            }

            var contract = asset.Contract;
            if (contract == null)
            {
                TempData["Notifyinfo"] = "العقد المرتبط بالأصل غير موجود";
                return RedirectToAction("Index");
            }

            try
            {
             
                paymentRepo.Delete(payment);
                await _unitOfWork.CompleteAsync();

                TempData["NotifySuccess"] = "تم حذف سجل الدفع واسترجاع البيانات بنجاح";
            }
            catch (Exception ex)
            {
                TempData["Notifyinfo"] = "حدث خطأ أثناء الحذف: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
