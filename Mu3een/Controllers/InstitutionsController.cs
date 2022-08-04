using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Extensions;
using Mu3een.IServices;
using Mu3een.Models;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InstitutionsController : ControllerBase
    {

        private readonly IInstitutionService _institutionService;

        public InstitutionsController(IInstitutionService institutionService)
        {
            _institutionService = institutionService;
        }

        /// <summary>
        /// Login institution
        /// </summary>
        /// <param name="model"></param>
        /// <returns>InstitutionLoginResponseModel</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<InstitutionLoginResponseModel>> Login(InstitutionLoginRequestModel model)
        {
            return Ok(await _institutionService.Login(model.Email!, model.Passowrd!));
        }

        /// <summary>
        /// Register Institution
        /// </summary>
        /// <param name="model"></param>
        /// <returns>InstitutionLoginResponseModel</returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<InstitutionLoginResponseModel>> Register([FromForm] InstitutionRegisterModel model)
        {
            return Ok(await _institutionService.Register(model));
        }

        /// <summary>
        /// Register Institution
        /// </summary>
        /// <param name="model"></param>
        /// <returns>InstitutionModel</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<InstitutionModel>> Put(Guid id, [FromForm] InstitutionRegisterModel model)
        {
            return Ok(await _institutionService.Update(id, model));
        }

        /// <summary>
        /// Register Institution
        /// </summary>
        /// <param name="model"></param>
        /// <returns>InstitutionModel</returns>
        [HttpPut("me")]
        public async Task<ActionResult<InstitutionModel>> Put([FromForm] InstitutionRegisterModel model)
        {
            return Ok(await _institutionService.Update(User.GetUserId(), model));
        }

        /// <summary>
        /// Register Institution
        /// </summary>
        /// <returns>InstitutionLoginResponseModel</returns>
        [HttpGet]
        public async Task<ActionResult<PagedList<InstitutionModel>>> Get([FromQuery] InstitutionSearchModel model)
        {

            var result = await _institutionService.GetAll(model);

            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);

            return Ok(result);
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
        /// Register Institution
        /// </summary>
        /// <param name="id"></param>
        /// <returns>InstitutionLoginResponseModel</returns>
        [HttpGet("me")]
        public async Task<ActionResult<InstitutionModel>> Get()
        {
            return Ok(await _institutionService.GetInstitutionById(User.GetUserId()));
        }

        /// <summary>
        /// Get Institution Rewards
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable RewardModel</returns>
        [HttpGet("{id}/Rewards")]
        public async Task<ActionResult<PagedList<RewardModel>>> GetRewards(Guid id, [FromQuery] PaginationParams model)
        {
            var result = await _institutionService.GetRewardsById(id, model);
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result);
        }

        /// <summary>
        /// Get Institution Rewards
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable RewardModel</returns>
        [HttpGet("me/Rewards")]
        public async Task<ActionResult<PagedList<RewardModel>>> GetRewards([FromQuery] PaginationParams model)
        {
            var result = await _institutionService.GetRewardsById(User.GetUserId(), model);
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result);
        }

        /// <summary>
        /// Get Institution Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable SocialEventModel</returns>
        [HttpGet("{id}/SocialEvents")]
        public async Task<ActionResult<IEnumerable<SocialEventModel>>> GetSocialEvents(Guid id,[FromQuery] PaginationParams model)
        {
            var result = await _institutionService.GetSocialEventsById(id, model);
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result);
        }

        /// <summary>
        /// Get Institution Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable SocialEventModel</returns>
        [HttpGet("me/SocialEvents")]
        public async Task<ActionResult<IEnumerable<SocialEventModel>>> GetSocialEvents(PaginationParams model)
        {
            var result = await _institutionService.GetSocialEventsById(User.GetUserId(), model);
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result);
        }

    }
}
