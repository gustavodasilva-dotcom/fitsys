using System.Net;
using Application.PersonalTrainers.Commands.CreatePersonalTrainer;
using Application.PersonalTrainers.Commands.UpdatePersonalTrainer;
using Application.PersonalTrainers.Queries.GetAllPersonalTrainers;
using Application.PersonalTrainers.Queries.GetPersonalTrainerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Entities;
using Web.Models.Global;

namespace Web.Controllers
{
    public class PersonalTrainersController(IMediator mediator) : Controller
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
                result.data = await _mediator.Send(new GetAllPersonalTrainersQuery());
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
                result.data = await _mediator.Send(new GetPersonalTrainerByIdQuery(UID));
            }
            catch (Exception e)
            {
                result.statusCode = HttpStatusCode.BadRequest;
                result.message = e.Message;
            }
            
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Insert([FromBody] PersonalTrainerInputModel data)
        {
            JsonResultViewModel result = new();

            try
            {
                result.data = await _mediator.Send(new CreatePersonalTrainerCommand(
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

        [HttpPut]
        public async Task<JsonResult> Update(Guid UID, [FromBody] PersonalTrainerInputModel data)
        {
            JsonResultViewModel result = new();

            try
            {
                result.data = await _mediator.Send(new UpdatePersonalTrainerCommand(
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
}
