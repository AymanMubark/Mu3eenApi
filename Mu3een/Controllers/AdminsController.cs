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
        public async Task<ActionResult<AdminModel>> Post(Admin model)
        {
            return Ok(await _adminService.Add(model));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminModel>>> Get([FromQuery] AdminSearchModel model)
        {
            return Ok(await _adminService.GetAll(model));
        }

        [HttpPut]
        public async Task<ActionResult<AdminModel>> Put(Guid id,AdminUpdateRequestModel model)
        {
            return Ok(await _adminService.Update(id,model,baseUrl));
        }
    }
}
