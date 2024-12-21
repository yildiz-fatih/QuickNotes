using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickNotes.Business.DTOs.Account;
using QuickNotes.Business.Services;
using QuickNotes.Web.ViewModels.Account;

namespace QuickNotes.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var registerResult = await _accountService.RegisterAsync(new RegisterRequest()
            {
                FullName = viewModel.FullName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Password = viewModel.Password,
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
    public async Task<IActionResult> LogIn(LogInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var loginResult =
                await _accountService.LogInAsync(new LogInRequest()
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
        await _accountService.LogOutAsync();
        
        return RedirectToAction("Index", "Home");
    }

    public IActionResult PromoteToAdmin()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PromoteToAdmin(PromoteToAdminViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var result = await _accountService.PromoteToAdminAsync(new PromoteToAdminRequest()
        {
            AppUserId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            AdminSecretKey = viewModel.AdminSecretKey
        });
        
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid secret key");
            return View(viewModel);
        }
        
        return RedirectToAction("Index", "Home");
    }
}