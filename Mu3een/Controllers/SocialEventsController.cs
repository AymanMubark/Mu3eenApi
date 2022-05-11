using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Models;
using Mu3een.Services;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialEventsController : ControllerBase
    {
        private readonly ISocialEventService _socialEventService;
        private readonly IHttpContextAccessor _contextAccessor;
        private string baseUrl;

        public SocialEventsController(ISocialEventService SocialEventService, IHttpContextAccessor contextAccessor)
        {
            _socialEventService = SocialEventService;
            _contextAccessor = contextAccessor;
            var request = _contextAccessor.HttpContext!.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult> Post([FromForm] SocialEventAddRequestModel model)
        {
            await _socialEventService.Add(model, baseUrl);
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialEventModel>>> Get( [FromQuery] SocialEventSearchModel model)
        {
            return Ok(await _socialEventService.GetAll(model));
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult> ApplyToEvent(Guid id, [FromBody] Guid volunteerId)
        {
            await _socialEventService.ApplyTo(id, volunteerId);
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
