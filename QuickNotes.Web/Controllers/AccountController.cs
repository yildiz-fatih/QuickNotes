using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickNotes.Business.DTOs.Account.Requests;
using QuickNotes.Business.Services.Interfaces;
using QuickNotes.Web.ViewModels.Account;

namespace QuickNotes.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var registerResponse = await _accountService.RegisterAsync(new RegisterRequest()
        {
            FullName = viewModel.FullName,
            UserName = viewModel.UserName,
            Email = viewModel.Email,
            Password = viewModel.Password,
        });
        
        if (registerResponse.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        
        foreach (var error in registerResponse.Errors)
        {
            ModelState.AddModelError(error.Code ?? string.Empty, error.Description);
        }
        
        return View(viewModel);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var loginResponse = await _accountService.LoginAsync(new LoginRequest()
        {
            Email      = viewModel.Email,
            Password   = viewModel.Password,
            RememberMe = viewModel.RememberMe
        });

        if (loginResponse.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
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

        var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var response = await _accountService.PromoteToAdminAsync(new PromoteToAdminRequest
        {
            AppUserId = userId,
            AdminSecretKey = viewModel.AdminSecretKey
        });

        if (!response.Succeeded)
        {
            var firstError = response.Errors.FirstOrDefault();
            ModelState.AddModelError(string.Empty, firstError?.Description ?? "Invalid secret key");
            return View(viewModel);
        }

        return RedirectToAction("Index", "Home");
    }
}