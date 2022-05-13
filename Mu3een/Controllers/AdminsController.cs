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
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
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
    }
}
