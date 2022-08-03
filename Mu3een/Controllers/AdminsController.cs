using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Entities;
using Mu3een.Extensions;
using Mu3een.IServices;
using Mu3een.Models;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AdminLoginResponseModel>> Login(AdminLoginRequestModel model)
        {
            return Ok(await _adminService.Login(model));
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<AdminModel>> Post(AdminRequestModel model)
        {
            return Ok(await _adminService.Add(model));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<AdminModel>>> Get([FromQuery] AdminSearchModel model)
        {
            var admins = await _adminService.GetAll(model);
            Response.AddPaginationHeader(admins.CurrentPage, admins.PageSize, admins.TotalCount, admins.TotalPages);
            return Ok(admins);
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Get( Guid id)
        {
            return Ok(await _adminService.GetById(id));
        }


        [HttpPut("{id}")]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<AdminModel>> Put(Guid id, [FromForm] AdminUpdateRequestModel model)
        {
            return Ok(await _adminService.Update(id, model));
        }

        [HttpGet("AdminCountsReport")]
        public async Task<ActionResult<AdminCountsReportModel>> GetAdminCountsReport()
        {
            return Ok(await _adminService.GetAdminCountsReport());
        }

        [HttpGet("SocailEventsReport")]
        public async Task<ActionResult<SocailEventsReport>> GetSocailEventsReport()
        {
            return Ok(await _adminService.GetSocailEventsReport());
        }
    }
}
