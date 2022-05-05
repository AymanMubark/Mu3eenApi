using Microsoft.AspNetCore.Mvc;
using Mu3een.Entities;
using Mu3een.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialEventTypesController : ControllerBase
    {
        private readonly ISocialEventTypeService _SocialEventTypeService;

        public SocialEventTypesController(ISocialEventTypeService SocialEventTypeService)
        {
            _SocialEventTypeService = SocialEventTypeService;
        }

        // GET: api/<SocialEventTypesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialEventType>>> Get()
        {
            return Ok(await _SocialEventTypeService.GetAll());
        }

        // GET api/<SocialEventTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialEventType>> Get(Guid id)
        {
            return Ok(await _SocialEventTypeService.GetById(id));
        }

        // POST api/<SocialEventTypesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string name)
        {
            await _SocialEventTypeService.Add(name);
            return Ok();
        }

        // PUT api/<SocialEventTypesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] string value)
        {
            await _SocialEventTypeService.Update(id, value);
            return Ok();
        }

        // DELETE api/<SocialEventTypesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _SocialEventTypeService.Delete(id);
            return Ok();
        }
    }
}
