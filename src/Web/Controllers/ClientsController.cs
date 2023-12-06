using Application.Clients.Commands.CreateClient;
using Application.Clients.Commands.UpdateClient;
using Application.Clients.Queries.GetAllClients;
using Application.Clients.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;
using Web.Models.Entities;
using Web.Models.Global;

namespace Web.Controllers
{
    public class ClientsController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            JsonResultViewModel result = new();

            try
            {
                result.data = await _mediator.Send(new GetAllClientsQuery());
            }
            catch (Exception e)
            {
                result.statusCode = HttpStatusCode.BadRequest;
                result.message = e.Message;
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> Get(Guid UID)
        {
            JsonResultViewModel result = new();

            try
            {
                result.data = await _mediator.Send(new GetClientByIdQuery(UID));
            }
            catch (Exception e)
            {
                result.statusCode = HttpStatusCode.BadRequest;
                result.message = e.Message;
            }
            
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Insert([FromBody] PersonInputModel data)
        {
            JsonResultViewModel result = new();

            try
            {
                result.data = await _mediator.Send(new CreateClientCommand(
                    Name: data.user.name,
                    Email: data.user.email,
                    Password: data.user.password,
                    Weight: data.client.weight,
                    Height: data.client.height,
                    Birthday: data.client.birthday,
                    Profile: data.profile
                ));
            }
            catch (Exception e)
            {
                result.statusCode = HttpStatusCode.BadRequest;
                result.message = e.Message;
            }

            return Json(result);
        }

        [HttpPut]
        public async Task<JsonResult> Update(Guid UID, [FromBody] PersonInputModel data)
        {
            JsonResultViewModel result = new();

            try
            {
                result.data = await _mediator.Send(new UpdateClientCommand(
                    UID: UID,
                    Name: data.user.name,
                    Email: data.user.email,
                    Password: data.user.password,
                    Weight: data.client.weight,
                    Height: data.client.height,
                    Birthday: data.client.birthday,
                    Profile: data.profile
                ));
            }
            catch (Exception e)
            {
                result.statusCode = HttpStatusCode.BadRequest;
                result.message = e.Message;
            }
            
            return Json(result);
        }
    }
}
