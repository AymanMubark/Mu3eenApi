using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Models;
using Mu3een.Services;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialServicesController : ControllerBase
    {
        private readonly SocialServiceService _SocialServiceService;
        private readonly IHttpContextAccessor _contextAccessor;
        private string baseUrl;

        public SocialServicesController(SocialServiceService SocialServiceService, IHttpContextAccessor contextAccessor)
        {
            _SocialServiceService = SocialServiceService;
            _contextAccessor = contextAccessor;
            var request = _contextAccessor.HttpContext!.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] SocialServiceAddRequestModel model)
        {
            await _SocialServiceService.Add(model, baseUrl);
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialServiceModel>>> Get()
        {
            return Ok(await _SocialServiceService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SocialServiceModel>> Get(Guid id)
        {
            return Ok(await _SocialServiceService.GetSocialServiceById(id));
        }

        [HttpGet("{id}/Volunteers")]
        public async Task<ActionResult<IEnumerable<VolunteerModel>>> GetServicesVolunteersById(Guid id)
        {
            return Ok(await _SocialServiceService.GetServicesVolunteersById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _SocialServiceService.Delete(id);
            return Ok();
        }

    }
}
