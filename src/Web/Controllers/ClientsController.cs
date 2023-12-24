using Application.Clients.Commands.CreateClient;
using Application.Clients.Commands.UpdateClient;
using Application.Clients.Queries.GetAllClients;
using Application.Clients.Queries.GetClientById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Models.Global;

namespace Web.Controllers;

public class ClientsController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Details()
    {
        return View();
    }

    [Authorize]
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

    [Authorize]
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

    [Authorize]
    [HttpPost]
    public async Task<JsonResult> Insert([FromBody] ClientInputModel data)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new CreateClientCommand(
                Name: data.person.name,
                Email: data.user.email,
                Password: data.user.password,
                Weight: data.weight,
                Height: data.height,
                Birthday: data.person.birthday,
                Profile: data.person.profile,
                Workouts: data.workouts
            ));
        }
        catch (Exception e)
        {
            result.statusCode = HttpStatusCode.BadRequest;
            result.message = e.Message;
        }

        return Json(result);
    }

    [Authorize]
    [HttpPut]
    public async Task<JsonResult> Update(Guid UID, [FromBody] ClientInputModel data)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new UpdateClientCommand(
                UID: UID,
                Name: data.person.name,
                Email: data.user.email,
                Password: data.user.password,
                Weight: data.weight,
                Height: data.height,
                Birthday: data.person.birthday,
                Profile: data.person.profile,
                Workouts: data.workouts
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
