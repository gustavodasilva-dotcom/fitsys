using System.Net;
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.UpdateEmployee;
using Application.Employees.Queries.GetAllEmployees;
using Application.Employees.Queries.GetEmployeeById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Global;

namespace Web.Controllers;

public class EmployeesController(IMediator mediator) : Controller
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
    public IActionResult _modalWorkoutRoutine()
    {
        return PartialView();
    }

    [Authorize]
    [HttpGet]
    public async Task<JsonResult> GetAll()
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new GetAllEmployeesQuery());
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
            result.data = await _mediator.Send(new GetEmployeeByIdQuery(UID));
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
    public async Task<JsonResult> Insert([FromBody] EmployeeInputModel data)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new CreateEmployeeCommand(
                Shifts: data.shifts,
                Name: data.person.name,
                Birthday: data.person.birthday,
                Profile: data.person.profile,
                Email: data.user.email,
                Password: data.user.password
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
    public async Task<JsonResult> Update(Guid UID, [FromBody] EmployeeInputModel data)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new UpdateEmployeeCommand(
                UID: UID,
                Shifts: data.shifts,
                Name: data.person.name,
                Birthday: data.person.birthday,
                Profile: data.person.profile,
                Email: data.user.email,
                Password: data.user.password
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
