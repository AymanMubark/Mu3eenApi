using Microsoft.AspNetCore.Mvc;
using Mu3een.Entities;
using Mu3een.IServices;

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
        public async Task<ActionResult> Post(SocialEventType model)
        {
            await _SocialEventTypeService.Add(model);
            return Ok();
        }

        // PUT api/<SocialEventTypesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, SocialEventType model)
        {
            await _SocialEventTypeService.Update(id, model);
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
