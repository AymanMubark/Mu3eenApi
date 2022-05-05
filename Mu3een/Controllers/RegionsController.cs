using Microsoft.AspNetCore.Mvc;
using Mu3een.Entities;
using Mu3een.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mu3een.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionService _regionService;
        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        // GET: api/<RegionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> Get()
        {
            return Ok(await _regionService.GetAll());
        }

        // GET api/<RegionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> Get(Guid id)
        {
            return Ok(await _regionService.GetById(id));
        }

        // POST api/<RegionsController>
        [HttpPost]
        public async Task<ActionResult> Post(Region model)
        {
            await _regionService.Add(model);
            return Ok();
        }

        // PUT api/<RegionsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, Region model)
        {
            await _regionService.Update(id, model);
            return Ok();
        }

        // DELETE api/<RegionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _regionService.Delete(id);
            return Ok();
        }
    }
}
