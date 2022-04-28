using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Models;
using Mu3een.Services;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IProviderService _providerService;

        public ProvidersController(IProviderService providerService, IHttpContextAccessor httpContextAccessor)
        {
            _providerService = providerService;
            _contextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Login provider
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ProviderLoginResponseModel</returns>
        [HttpPost("Login")]
        public async Task<ActionResult<ProviderLoginResponseModel>> Login(ProviderLoginRequestModel model) 
        {
            return Ok(await _providerService.Login(model.Email,model.Passowrd));
        }

        /// <summary>
        /// Register Provider
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ProviderLoginResponseModel</returns>
        [HttpPost("Register")]
        public async Task<ActionResult<ProviderLoginResponseModel>> Register([FromForm] ProviderRegisterModel model) 
        {
            var request = _contextAccessor.HttpContext!.Request;
            return Ok(await _providerService.Register(model, $"{request.Scheme}://{request.Host}"));
        } 
        
        /// <summary>
        /// Register Provider
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ProviderLoginResponseModel</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProviderModel>> Get(Guid id) 
        {
            return Ok(await _providerService.GetProviderById(id));
        }

        /// <summary>
        /// Get Provider Rewards
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable RewardModel</returns>
        [HttpGet("{id}/Rewards")]
        public async Task<ActionResult<IEnumerable<RewardModel>>> GetRewards(Guid id) 
        {
            return Ok(await _providerService.GetRewardsById(id));
        }

        /// <summary>
        /// Get Provider Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable SocialServiceModel</returns>
        [HttpGet("{id}/Services")]
        public async Task<ActionResult<IEnumerable<SocialServiceModel>>> GetServices(Guid id) 
        {
            return Ok(await _providerService.GetSocialServicesById(id));
        }

    }
}
