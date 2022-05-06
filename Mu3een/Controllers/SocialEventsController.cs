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
        private readonly ISocialEventService _SocialEventService;
        private readonly IHttpContextAccessor _contextAccessor;
        private string baseUrl;

        public SocialEventsController(ISocialEventService SocialEventService, IHttpContextAccessor contextAccessor)
        {
            _SocialEventService = SocialEventService;
            _contextAccessor = contextAccessor;
            var request = _contextAccessor.HttpContext!.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult> Post([FromForm] SocialEventAddRequestModel model)
        {
            await _SocialEventService.Add(model, baseUrl);
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialEventModel>>> Get()
        {
            return Ok(await _SocialEventService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SocialEventModel>> Get(Guid id)
        {
            return Ok(await _SocialEventService.GetSocialEventById(id));
        }

        [HttpGet("{id}/Volunteers")]
        public async Task<ActionResult<IEnumerable<VolunteerModel>>> GetServicesVolunteersById(Guid id)
        {
            return Ok(await _SocialEventService.GetServicesVolunteersById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _SocialEventService.Delete(id);
            return Ok();
        }

    }
}
