using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickNotes.Business.DTOs.User;
using QuickNotes.Business.Services;
using QuickNotes.Data.Entities;
using QuickNotes.Web.ViewModels.User;

namespace QuickNotes.Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly RoleManager<AppRole> _roleManager;

    public UserController(IUserService userService, RoleManager<AppRole> roleManager)
    {
        _userService = userService;
        _roleManager = roleManager;
    }
    
    public async Task<IActionResult> SignUp()
    {
        // Retrieve all role names from the database
        var roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        ViewBag.Roles = new SelectList(roles, "Name", "Name");

        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var registerResult = await _userService.RegisterAsync(new RegisterUserRequest()
            {
                FullName = viewModel.FullName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                RoleSelected = viewModel.RoleSelected
            });
            
            if (registerResult.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                registerResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
        }
        
        return View(viewModel);
    }

    public IActionResult LogIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var loginResult =
                await _userService.LoginAsync(new LoginUserRequest()
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    RememberMe = viewModel.RememberMe,
                });
            if (loginResult.Succeeded)
            {
                return RedirectToAction("Index", "Note");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }
        
        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await _userService.LogOutAsync();
        
        return RedirectToAction("Index", "Home");
    }
}