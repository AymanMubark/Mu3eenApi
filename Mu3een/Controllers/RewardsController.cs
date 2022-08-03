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
    public class RewardsController : ControllerBase
    {
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult> Post([FromForm] RewardAddRequestModel model)
        {
            await _rewardService.Add(model);
            return Ok();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RewardModel>>> Get([FromQuery] RewardSearchModel model)
        {
            return Ok(await _rewardService.GetAll(model));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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

        [HttpPost("{id}/Redeem")]
        public async Task<ActionResult> Redeem(Guid id)
        {
            await _rewardService.Redeem(id, User.GetUserId());
            return Ok();
        }


    }
}
