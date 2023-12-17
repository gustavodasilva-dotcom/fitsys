using Application.Files.Commands.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Models.Global;

namespace Web.Controllers;

public class FilesController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [Authorize]
    [HttpPost]
    public async Task<JsonResult> Upload(IFormCollection form)
    {
        JsonResultViewModel result = new();

        try
        {
            result.data = await _mediator.Send(new UploadFileCommand(form));
        }
        catch (Exception e)
        {
            result.statusCode = HttpStatusCode.BadRequest;
            result.message = e.Message;
        }

        return Json(result);
    }
}
