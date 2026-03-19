using Gis_Project.Models;
using Gis_Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gis_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVM)
        {

            var existingUser = await userManager.FindByEmailAsync(userRegisterVM.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "البريد الإلكتروني مسجل بالفعل");
                return View(userRegisterVM);
            }

            if (ModelState.IsValid)
            {    //Mapping 
                ApplicationUser user = new ApplicationUser();
                user.UserName = userRegisterVM.Email.Split("@")[0];
                user.Email = userRegisterVM.Email;
                user.FUllName = userRegisterVM.FullName;

                //Save user to database 
                IdentityResult result = await userManager.CreateAsync(user, userRegisterVM.Password);
                if (result.Succeeded)
                {
                    //assign Role
                    await userManager.AddToRoleAsync(user, "NewUser");

                    //Cookies
                    //  await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(userRegisterVM);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                //check found 
                ApplicationUser user = await userManager.FindByNameAsync(loginVM.UserName);

                if (user == null)
                {
                    user = await userManager.FindByEmailAsync(loginVM.UserName);
                }

                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (found)
                    { //Add Claim

                        if (user.IsBlocked == true)
                        {
                            ModelState.AddModelError(string.Empty, "تم حظر المستخدم");
                            return View(loginVM);
                        }

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim("FullName", user.FUllName)
                        };
                        //create Cookie 
                        await signInManager.SignInWithClaimsAsync(user, loginVM.RememberMe, claims);

                        if (Url.IsLocalUrl(returnUrl))
                            return LocalRedirect(returnUrl);
                        else
                            return RedirectToAction("Index", "Home");

                    }
                }

                ModelState.AddModelError(string.Empty, "اسم المستخدم أو البريد الإلكتروني أو كلمة المرور غير صحيحة");

            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
