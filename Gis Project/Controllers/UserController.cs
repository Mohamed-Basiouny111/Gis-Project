using Gis_Project.Models;
using Gis_Project.Repositories;
using Gis_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gis_Project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var model = new clsData_VM();

            model.Roles = _roleManager.Roles.ToList();

            var users = new List<UserVM>();

            var usersList = _userManager.Users.Where(x => x.FUllName != "Admin").ToList();

            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);

                users.Add(new UserVM
                {
                    Id = user.Id,
                    FullName = user.FUllName,
                    Email = user.Email,
                    Region = user.Region,
                    UserRoles = roles.ToList(),
                    Block = Convert.ToBoolean(user.IsBlocked),
                });
            }

            model.Users = users;

            return View(model);
        }


        public async Task<IActionResult> ToggleUserRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.AddToRoleAsync(user, roleName);
            TempData["NotifySuccess"] = "تم إضافة هذة الصلاحية للمستخدم بنجاح";
            TempData["NotifyTitle"] = "عملية ناجحة .";
            if (!result.Succeeded)
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);

                TempData["NotifySuccess"] = "تم حذف هذة الصلاحية من المستخدم بنجاح";
                TempData["NotifyTitle"] = "عملية ناجحة .";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUserBlock(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["NotifyError"] = "المستخدم غير موجود";
                return RedirectToAction("Index");
            }

            user.IsBlocked = !user.IsBlocked;

            await _userManager.UpdateAsync(user);

            if (user.IsBlocked == true)
            {
                TempData["NotifySuccess"] = "تم حظر المستخدم بنجاح";
            }
            else
            {
                TempData["NotifySuccess"] = "تم إلغاء حظر المستخدم بنجاح";
            }

            TempData["NotifyTitle"] = "عملية ناجحة.";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateRegion(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var model = new UserRegionVM
            {
                UserId = user.Id,
                Email = user.Email,
                RegionId = user.Region?.Id
            };

            var repo = _unitOfWork.GetRepository<Region, int>();
            var Regions = (await repo.GetAllAsync()).OrderByDescending(r => r.Id).ToList();

            ViewBag.Regions = Regions.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name,
                Selected = r.Id == user.Region?.Id
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdate(string userId, int RegionId)
         {
            if (!ModelState.IsValid)
                return View("UpdateRegion", userId);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["NotifyError"] = "المستخدم غير موجود";
                return RedirectToAction("Index");
            }

            user.RegionId = RegionId;

            await _userManager.UpdateAsync(user);

            TempData["NotifySuccess"] = "تم تعديل المنطقة بنجاح";
            TempData["NotifyTitle"] = "عملية ناجحة.";

            return RedirectToAction("Index");
        }

    }
}
