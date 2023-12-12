using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yummi.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yummi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private readonly IUnitOfWork _uOw;
        private readonly IMediator _mediator;
        public MeasureController(IUnitOfWork uOw, IMediator mediator)
        {
            _uOw = uOw;
            _mediator = mediator;
        }
        // GET: api/<MeasureController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MeasureController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MeasureController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] string value)
        {
            
            return Ok();
        }

        // PUT api/<MeasureController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MeasureController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
