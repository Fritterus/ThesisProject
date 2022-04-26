using CarSharing.Entities.DataBaseModels;
using CarSharing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarSharing.Controllers
{
    public class UsersAccountController : Controller
    {
        private UserManager<User> _userManager;

        public UsersAccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> UsersAccount()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }

        public async Task<IActionResult> ChangePassword()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> Replenish(int sum)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            user.Money += sum;
            return View(user.Money);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UsersAccount");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "");
                }
            }
            return View(model);
        }
    }
}
