using Microsoft.AspNetCore.Mvc;
using QuickNotes.Business.DTOs.User;
using QuickNotes.Business.Services;
using QuickNotes.Web.ViewModels.User;

namespace QuickNotes.Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
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
            var registerResult = await _userService.RegisterAsync(new RegisterUserRequest()
            {
                FullName = viewModel.FullName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Password = viewModel.Password,
            });
            
            if (registerResult.Succeeded)
                return RedirectToAction("Index");
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
                    Persistent = viewModel.Persistent,
                    Lock = viewModel.Lock
                });
            if (loginResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }
        
        return View(viewModel);
    }
}