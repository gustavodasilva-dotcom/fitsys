using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                // var user = await _mediator.Send(new GetUserByEmailQuery(email));

                // if (!BCrypt.Net.BCrypt.Verify(password, user.user.password))
                //     throw new Exception("Senha incorreta");

                return Redirect("/Home/Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            try
            {
                //await _mediator.Send(new CreateUserCommand(name, email, password));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            return View();
        }
    }
}
