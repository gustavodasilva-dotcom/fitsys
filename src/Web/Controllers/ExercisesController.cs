using System.Net;
using Application.Exercises.Commands.CreateExercise;
using Application.Exercises.Commands.UpdateExercise;
using Application.Exercises.Queries.GetAllExercises;
using Application.Exercises.Queries.GetExerciseById;
using Domain.Entities.Partials;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Global;

namespace Web.Controllers;

public class ExercisesController(IMediator mediator) : Controller
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
            result.data = await _mediator.Send(new GetAllExercisesQuery());
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
            result.data = await _mediator.Send(new GetExerciseById(UID));
        }
        catch (Exception e)
        {
            result.statusCode = HttpStatusCode.BadRequest;
            result.message = e.Message;
        }

        return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> Insert([FromBody] Models.Entities.ExerciseInputModel data)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new CreateExerciseCommand(
                Name: data.name,
                Steps: new QuillEditor()
                {
                    ops = data.steps.ops.Select(op => new Op()
                    {
                        insert = op.insert,
                        attributes = op.attributes != null ? new Attributes()
                        {
                            list = op.attributes.list
                        } : null
                    }).ToList()
                },
                MuscleGroups: data.muscleGroups
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
    public async Task<JsonResult> Update(Guid UID, [FromBody] Models.Entities.ExerciseInputModel data)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new UpdateExerciseCommand(
                UID: UID,
                Name: data.name,
                Steps: new QuillEditor()
                {
                    ops = data.steps.ops.Select(op => new Op()
                    {
                        insert = op.insert,
                        attributes = op.attributes != null ? new Attributes()
                        {
                            list = op.attributes.list
                        } : null
                    }).ToList()
                },
                MuscleGroups: data.muscleGroups
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
