using Gis_Project.Models;
using Gis_Project.Repositories;
using Gis_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gis_Project.Controllers
{
    public class RegionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var repo = _unitOfWork.GetRepository<Region, int>();

            var model = new clsData_VM
            {
                Regions = (await repo.GetAllAsync()).OrderByDescending(r => r.Id).ToList()
            };

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCreate(Region region)
        {
            if (region == null)
            {
                TempData["NotifyError"] = "البيانات غير صالحة";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var repo = _unitOfWork.GetRepository<Region, int>();

                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                region.CreatedById = userId;

                await repo.AddAsync(region);
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
            var repo = _unitOfWork.GetRepository<Region, int>();
            var region = await repo.GetByIdAsync(id);

            if (region == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction("Index");
            }

            return View("Update", region);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Region region)
        {
            var repo = _unitOfWork.GetRepository<Region, int>();
            var existingRegion = await repo.GetByIdAsync(region.Id);

            if (existingRegion == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
                return View("Update", region);

            existingRegion.Name = region.Name;
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            existingRegion.UpdatedById = userId;
            existingRegion.UpdatedAt = DateTime.UtcNow;
            repo.Update(existingRegion);
            await _unitOfWork.CompleteAsync();

            TempData["NotifySuccess"] = "تم تعديل العنصر بنجاح";
            TempData["NotifyTitle"] = "عملية ناجحة.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var repo = _unitOfWork.GetRepository<Region, int>();

            var existingRegion = await repo.GetByIdAsync(id);
            if (existingRegion == null)
            {
                TempData["NotifyError"] = "العنصر غير موجود";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                repo.Delete(existingRegion);
                await _unitOfWork.CompleteAsync();

                TempData["NotifySuccess"] = "تم حذف العنصر بنجاح";
                TempData["NotifyTitle"] = "عملية ناجحة.";
            }
            catch (Exception ex)
            {
                TempData["NotifyError"] = "حدث خطأ أثناء الحذف: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
