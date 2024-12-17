using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickNotes.Data.Entities;
using QuickNotes.Web.ViewModels.User;

namespace QuickNotes.Web.Controllers;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(AppUserViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var appUser = new AppUser()
            {
                FullName = viewModel.FullName,
                UserName = viewModel.UserName,
                Email = viewModel.Email
            };
            
            var result = await _userManager.CreateAsync(appUser, viewModel.Password);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
        }
        
        return View(viewModel);
    }
}