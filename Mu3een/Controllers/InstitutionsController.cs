using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Models;
using Mu3een.Services;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IInstitutionService _institutionService;

        public InstitutionsController(IInstitutionService institutionService, IHttpContextAccessor httpContextAccessor)
        {
            _institutionService = institutionService;
            _contextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Login institution
        /// </summary>
        /// <param name="model"></param>
        /// <returns>InstitutionLoginResponseModel</returns>
        [HttpPost("Login")]
        public async Task<ActionResult<InstitutionLoginResponseModel>> Login(InstitutionLoginRequestModel model) 
        {
            return Ok(await _institutionService.Login(model.Email,model.Passowrd));
        }

        /// <summary>
        /// Register Institution
        /// </summary>
        /// <param name="model"></param>
        /// <returns>InstitutionLoginResponseModel</returns>
        [HttpPost("Register")]
        public async Task<ActionResult<InstitutionLoginResponseModel>> Register([FromForm] InstitutionRegisterModel model) 
        {
            var request = _contextAccessor.HttpContext!.Request;
            return Ok(await _institutionService.Register(model, $"{request.Scheme}://{request.Host}"));
        } 
        
        /// <summary>
        /// Register Institution
        /// </summary>
        /// <param name="id"></param>
        /// <returns>InstitutionLoginResponseModel</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionModel>> Get(Guid id) 
        {
            return Ok(await _institutionService.GetInstitutionById(id));
        }

        /// <summary>
        /// Get Institution Rewards
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable RewardModel</returns>
        [HttpGet("{id}/Rewards")]
        public async Task<ActionResult<IEnumerable<RewardModel>>> GetRewards(Guid id) 
        {
            return Ok(await _institutionService.GetRewardsById(id));
        }

        /// <summary>
        /// Get Institution Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable SocialEventModel</returns>
        [HttpGet("{id}/SocialEvents")]
        public async Task<ActionResult<IEnumerable<SocialEventModel>>> GetServices(Guid id) 
        {
            return Ok(await _institutionService.GetSocialEventsById(id));
        }

    }
}
