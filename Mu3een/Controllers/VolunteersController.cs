﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Models;
using Mu3een.Services;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {
        private readonly IVolunteerService _volunteerService;
        private readonly IHttpContextAccessor _contextAccessor;


        public VolunteersController(IVolunteerService volunteerService, IHttpContextAccessor contextAccessor)
        {
            _volunteerService = volunteerService;
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Check phone add create new otp
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>OTP</returns>


        [HttpPost("VerifyPhone")]
        public async Task<ActionResult<string>> VerifyPhone([FromBody] string phone)
        {
            return Ok(await _volunteerService.Login(phone));
        }

        /// <summary>
        /// VerifyOTP of phone
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AuthToken</returns>
        [HttpPost("VerifyOTP")]
        public async Task<ActionResult<VerifyOTPResponseModel>> VerifyOTP(VerifyOTPModel model)
        {
            return Ok(await _volunteerService.VerifyOTP(model.Phone, model.OTP));
        }

        /// <summary>
        /// Get volunteer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>volunteer</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VolunteerModel>> Get(Guid id)
        {
            return Ok(await _volunteerService.GetVolunteerById(id));
        }

        /// <summary>
        /// Get Rewords
        /// </summary>
        /// <param name="id">volunteer Id</param>
        /// <returns></returns>
        [HttpGet("{id}/Rewords")]
        public async Task<ActionResult<IEnumerable<VolunteerRewardModel>>> GetRewords(Guid id)
        {
            return Ok(await _volunteerService.GetRewardsById(id));
        }


        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">volunteer Id</param>
        /// <returns></returns>
        [HttpGet("{id}/SocialEvents")]
        public async Task<ActionResult<IEnumerable<VolunteerSocialEventModel>>> GetSocialEvents(Guid id)
        {
            return Ok(await _volunteerService.GetSocialEventsById(id));
        } 
        
        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">volunteer Id</param>
        /// <returns></returns>
        [HttpPost("{id}/ApplyToEvent")]
        public async Task<ActionResult> ApplyToEvent(Guid id,[FromBody]Guid socialEventId)
        {
            await _volunteerService.ApplyToService(id, socialEventId);
            return Ok();
        }

        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">institution id</param>
        /// <param name="socialEventId">socialEventId Id</param>
        /// <returns></returns>
        [HttpPost("{id}/SetAcceptEvent")]
        public async Task<ActionResult> SetAcceptEvent(Guid id,[FromBody]Guid socialEventId)
        {
            await _volunteerService.SetAccept(id, socialEventId);
            return Ok();
        }

        /// <summary>
        /// Get Social Services
        /// </summary>
        /// <param name="id">institution id</param>
        /// <param name="socialEventId">socialEventId Id</param>
        /// <returns></returns>
        [HttpPost("{id}/SetCompletedEvent")]
        public async Task<ActionResult> SetCompletedEvent(Guid id,[FromBody]Guid socialEventId)
        {
            await _volunteerService.SetCompleted(id, socialEventId);
            return Ok();
        }

        /// <summary>
        /// Get ExChangePoints of volanter with reward
        /// </summary>
        /// <param name="id">institution id</param>
        /// <param name="socialEventId">socialEventId Id</param>
        /// <returns></returns>
        [HttpPost("{id}/ExChangePoints")]
        public async Task<ActionResult> ExChangePoints(Guid id,[FromBody]Guid rewardId)
        {
            await _volunteerService.ExChangePoints(id, rewardId);
            return Ok();
        }      
        
        /// <summary>
        /// Get ExChangePoints of volanter with reward
        /// </summary>
        /// <param name="id">institution id</param>
        /// <param name="socialEventId">socialEventId Id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<VolunteerModel>> Put(Guid id,VolunteerRegisterRequestModel model)
        {
          
            return Ok(await _volunteerService.Register(id, model));
        }

    }
}
