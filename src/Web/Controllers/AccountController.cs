using Application.Accounts.Commands.ExecuteLogin;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers;

public class AccountController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    public IActionResult Login()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        try
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        catch (Exception e)
        {
            ViewBag.Message = e.Message;
        }
        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe)
    {
        try
        {
            var claims = await _mediator.Send(new ExecuteLoginCommand(email, password));
            
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = rememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddDays(2)
                });

            return Redirect("/Home/Index");
        }
        catch (Exception e)
        {
            ViewBag.Message = e.Message;
        }
        return View();
    }
}
