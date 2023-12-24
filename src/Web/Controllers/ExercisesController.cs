using System.Net;
using Application.Exercises.Commands.CreateExercise;
using Application.Exercises.Commands.UpdateExercise;
using Application.Exercises.Queries.GetAllExercises;
using Application.Exercises.Queries.GetExerciseById;
using Domain.Entities.Partials;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Global;

namespace Web.Controllers;

public class ExercisesController(IMediator mediator) : Controller
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
            result.data = await _mediator.Send(new GetAllExercisesQuery());
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
            result.data = await _mediator.Send(new GetExerciseById(UID));
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
    public async Task<JsonResult> Insert([FromBody] Domain.Models.ExerciseInputModel data)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new CreateExerciseCommand(
                Name: data.name,
                Image: data.image,
                Steps: data.steps,
                MuscleGroups: data.muscleGroups,
                GymEquipments: data.gymEquipments
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
    public async Task<JsonResult> Update(Guid UID, [FromBody] Domain.Models.ExerciseInputModel data)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new UpdateExerciseCommand(
                UID: UID,
                Name: data.name,
                Image: data.image,
                Steps: data.steps,
                MuscleGroups: data.muscleGroups,
                GymEquipments: data.gymEquipments
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
