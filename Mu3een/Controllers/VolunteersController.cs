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
    public class VolunteersController : ControllerBase
    {
        private readonly IVolunteerService _volunteerService;

        public VolunteersController(IVolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
        }

        /// <summary>
        /// Check phone add create new otp
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>OTP</returns>


        [HttpPost("VerifyPhone")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> VerifyPhone([FromBody] string phone)
        {
            return Ok(await _volunteerService.VerifyPhone(phone));
        }

        /// <summary>
        /// VerifyOTP of phone
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AuthToken</returns>
        [HttpPost("VerifyOTP")]
        [AllowAnonymous]
        public async Task<ActionResult<VerifyOTPResponseModel>> VerifyOTP(VerifyOTPModel model)
        {
            return Ok(await _volunteerService.VerifyOTP(model.Phone, model.OTP));
        }

        /// <summary>
        /// Get volunteer by id
        /// </summary>
        /// <returns>volunteer</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VolunteerModel>>> Get([FromQuery] VolunteerSearchModel model)
        {
            return Ok(await _volunteerService.GetAll(model));
        }

        /// <summary>
        /// Get volunteer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>volunteer</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VolunteerModel>> Get(Guid id)
        {
            var model = await _volunteerService.GetVolunteerById(id);
            return Ok(model);
        }
        
        /// <summary>
        /// Get volunteer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>volunteer</returns>
        [HttpGet("me")]
        [Authorize(Roles = "Volunteer")]
        public async Task<ActionResult<VolunteerModel>> Get()
        {
            var model = await _volunteerService.GetVolunteerById(User.GetUserId());
            return Ok(model);
        }

        /// <summary>
        /// Get Rewords
        /// </summary>
        /// <param name="id">volunteer Id</param>
        /// <returns></returns>
        [HttpGet("{id}/Rewards")]
        public async Task<ActionResult<IEnumerable<RewardModel>>> GetRewords(Guid id)
        {
            return Ok(await _volunteerService.GetRewardsById(id));
        }
        
        /// <summary>
        /// Get Rewords
        /// </summary>
        /// <param name="id">volunteer Id</param>
        /// <returns></returns>
        [HttpGet("me/Rewards")]
        [Authorize(Roles = "Volunteer")]
        public async Task<ActionResult<IEnumerable<RewardModel>>> GetRewords()
        {
            return Ok(await _volunteerService.GetRewardsById(User.GetUserId()));
        }


        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">volunteer Id</param>
        /// <returns></returns>
        [HttpGet("{id}/SocialEvents")]
        public async Task<ActionResult<IEnumerable<SocialEventVolunteerModel>>> GetSocialEvents(Guid id)
        {
            return Ok(await _volunteerService.GetSocialEventsById(id));
        }

        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">volunteer Id</param>
        /// <returns></returns>
        [HttpGet("me/SocialEvents")]
        [Authorize(Roles = "Volunteer")]
        public async Task<ActionResult<IEnumerable<SocialEventVolunteerModel>>> GetSocialEvents()
        {
            return Ok(await _volunteerService.GetSocialEventsById(User.GetUserId()));
        }

        [HttpPut("me")]
        [RequestSizeLimit(long.MaxValue)]
        [Authorize(Roles = "Volunteer")]
        public async Task<ActionResult<VolunteerModel>> Register([FromForm] VolunteerRegisterRequestModel model)
        {
            return Ok(await _volunteerService.Register(User.GetUserId(), model));
        }

        [HttpPut("{id}")]
        [RequestSizeLimit(long.MaxValue)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<VolunteerModel>> Put(Guid id, [FromForm] VolunteerRegisterRequestModel model)
        {
            return Ok(await _volunteerService.Register(id, model));
        }
    }
}
