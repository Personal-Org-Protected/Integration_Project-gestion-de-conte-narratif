using Application.Common.Models;
using Application.Notifications.Command.CreateCommand;
using Application.Notifications.Command.DeleteCommand;
using Application.Notifications.Command.UpdateCommand;
using Application.Notifications.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{

    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class NotificationController : ApiController
    {

        [Authorize("ReadContent")]
        [HttpGet("{id}")]
        public async Task<PaginatedItems<NotificationDto>> GetById(int id)
        {
            return await Mediator.Send(new GetNotificationsByUserQueries(id));
        }

        [Authorize("ReadContent")]
        [HttpGet("count")]
        public async Task<int> Get()
        {
            return await Mediator.Send(new GetCountNotificationByUserQueries());
        }



        [Authorize("AdminAcces")]//utilisable seulment en interne dans l'applicatif
        [HttpPost("admin")]
        public async Task<ActionResult<Result>> PostAdmin(AdminSendMessageCommand adminSendMessage)
        {
            return await Mediator.Send(adminSendMessage);
        }

        [Authorize("ReadContent")]//utilisable seulment en interne dans l'applicatif
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateNotificationCommand createNotification)
        {
            return await Mediator.Send(createNotification);
        }


        [Authorize("ReadContent")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Put(int id,[FromQuery]UpdateNotificationCommand updateNotification)
        {
            if (id != updateNotification.NotifId)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateNotification);
        }



        [Authorize("ReadContent")]
        [HttpDelete("/{id}")]
        public async Task<ActionResult<Result>> Delete2(int id)
        {
            return await Mediator.Send(new DeleteNotificationCommand(id));
        }
    }
}
