using System.Net;
using Application.Constants.Queries.GetConstantByEnum;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Global;

namespace Web.Controllers;

public class ConstantsController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<JsonResult> Get(ConstantsEnum Constant)
    {
        JsonResultViewModel result = new();
        
        try
        {
            result.data = await _mediator.Send(new GetConstantByEnumQuery(Constant));
        }
        catch (Exception e)
        {
            result.statusCode = HttpStatusCode.BadRequest;
            result.message = e.Message;
        }

        return Json(result);
    }
}