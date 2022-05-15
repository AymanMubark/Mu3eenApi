using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mu3een.Entities;
using Mu3een.Models;
using Mu3een.Services;

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAdminService _adminService;
        private string baseUrl;

        public AdminsController(IAdminService adminService, IHttpContextAccessor contextAccessor)
        {
            _adminService = adminService;
            _contextAccessor = contextAccessor;
            var request = _contextAccessor.HttpContext!.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AdminLoginResponseModel>> Login(AdminLoginRequestModel model)
        {
            return Ok(await _adminService.Login(model));
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<AdminModel>> Post(AdminRequestModel model)
        {
            return Ok(await _adminService.Add(model, baseUrl));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminModel>>> Get([FromQuery] AdminSearchModel model)
        {
            return Ok(await _adminService.GetAll(model));
        }

        [HttpPut("{id}")]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<AdminModel>> Put(Guid id, [FromForm] AdminRequestModel model)
        {
            return Ok(await _adminService.Update(id, model, baseUrl));
        }

        [HttpGet("AdminCountsReport")]
        public async Task<ActionResult<AdminCountsReportModel>> GetAdminCountsReport()
        {
            return Ok(await _adminService.GetAdminCountsReport());
        }  

        [HttpGet("SocailEventsReport")]
        public async Task<ActionResult<AdminCountsReportModel>> GetSocailEventsReport()
        {
            return Ok(await _adminService.GetSocailEventsReport());
        }
    }
}
