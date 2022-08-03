using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Extensions;
using Mu3een.IServices;
using Mu3een.Models;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SocialEventsController : ControllerBase
    {
        private readonly ISocialEventService _socialEventService;

        public SocialEventsController(ISocialEventService SocialEventService)
        {
            _socialEventService = SocialEventService;
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Authorize(Roles = "Institution")]
        public async Task<ActionResult> Post([FromForm] SocialEventAddRequestModel model)
        {
            model.InstitutionId = User.GetUserId();
            await _socialEventService.Add(model);
            return Ok();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SocialEventModel>>> Get( [FromQuery] SocialEventSearchModel model)
        {
            return Ok(await _socialEventService.GetAll(model));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SocialEventModel>> Get(Guid id)
        {
            return Ok(await _socialEventService.GetSocialEventById(id));
        }

        [HttpGet("{id}/Volunteers")]
        public async Task<ActionResult<IEnumerable<SocialEventVolunteerModel>>> GetEventVolunteers(Guid id)
        {
            return Ok(await _socialEventService.GetEventVolunteers(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _socialEventService.Delete(id);
            return Ok();
        }


        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">volunteer Id</param>
        /// <returns></returns>
        [HttpPost("{id}/Apply")]
        [Authorize(Roles = "Volunteer")]
        public async Task<ActionResult> ApplyToEvent(Guid id)
        {
            await _socialEventService.ApplyTo(id, User.GetUserId());
            return Ok();
        }

        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">institution id</param>
        /// <param name="socialEventId">socialEventId Id</param>
        /// <returns></returns>
        [HttpPost("{id}/Accept")]
        public async Task<ActionResult> SetAcceptEvent(Guid id, [FromBody] Guid volunteerId)
        {
            await _socialEventService.SetAccept(id, volunteerId);
            return Ok();
        }

        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">institution id</param>
        /// <param name="socialEventId">socialEventId Id</param>
        /// <returns></returns>
        [HttpPost("{id}/Complete")]
        public async Task<ActionResult> SetCompletedEvent(Guid id, [FromBody] Guid volunteerId)
        {
            await _socialEventService.SetCompleted(id, volunteerId);
            return Ok();
        }

    }
}
