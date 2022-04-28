using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Models;
using Mu3een.Services;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
        private readonly RewardService _rewardService;
        private readonly IHttpContextAccessor _contextAccessor;
        private string baseUrl;

        public RewardsController(RewardService rewardService, IHttpContextAccessor contextAccessor)
        {
            _rewardService = rewardService;
            _contextAccessor = contextAccessor;
            var request = _contextAccessor.HttpContext!.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] RewardAddRequestModel model)
        {
            await _rewardService.Add(model, baseUrl);
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RewardModel>>> Get()
        {
            return Ok(await _rewardService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _rewardService.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _rewardService.Delete(id);
            return Ok();
        }

    }
}
