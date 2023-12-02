using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PersonalTrainersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
