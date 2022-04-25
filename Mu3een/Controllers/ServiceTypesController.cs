using Microsoft.AspNetCore.Mvc;
using Mu3een.Entities;
using Mu3een.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialServiceTypesController : ControllerBase
    {
        private readonly ISocialServiceTypeService _SocialServiceTypeService;

        public SocialServiceTypesController(ISocialServiceTypeService SocialServiceTypeService)
        {
            _SocialServiceTypeService = SocialServiceTypeService;
        }

        // GET: api/<SocialServiceTypesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialServiceType>>> Get()
        {
            return Ok(await _SocialServiceTypeService.GetAll());
        }

        // GET api/<SocialServiceTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialServiceType>> Get(Guid id)
        {
            return Ok(await _SocialServiceTypeService.GetById(id));
        }

        // POST api/<SocialServiceTypesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string name)
        {
            await _SocialServiceTypeService.Add(name);
            return Ok();
        }

        // PUT api/<SocialServiceTypesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] string value)
        {
            await _SocialServiceTypeService.Update(id, value);
            return Ok();
        }

        // DELETE api/<SocialServiceTypesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _SocialServiceTypeService.Delete(id);
            return Ok();
        }
    }
}
