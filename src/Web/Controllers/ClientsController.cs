using Application.Clients.Commands.CreateClient;
using Application.Clients.Queries.GetAllClients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Models;

namespace Web.Controllers
{
    public class ClientsController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            JsonResultViewModel result = new();
            try
            {
                result.Data = await _mediator.Send(new GetAllClientsQuery());
            }
            catch (Exception e)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = e.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Register(string name, string email, string password, decimal weight, decimal height, DateTime birthday)
        {
            JsonResultViewModel result = new();
            try
            {
                result.Data = await _mediator.Send(new CreateClientCommand(name, email, password, weight, height, birthday));
            }
            catch (Exception e)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = e.Message;
            }
            return Json(result);
        }
    }
}
