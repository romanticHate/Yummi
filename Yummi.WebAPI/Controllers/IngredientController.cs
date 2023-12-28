using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yummi.Application.CQRS.Ingredient.Cmmnd;
using Yummi.Application.CQRS.Ingredient.Qry;
using Yummi.Core.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yummi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {       
        private readonly IMapper _mapper;       
        private readonly IMediator _mediator;
        public IngredientController(IMapper mapper,          
            IMediator mediator)
        {          
            _mapper=mapper;          
            _mediator=mediator;
        }

        // GET: api/<IngredientController>
        [HttpGet]
        public async Task<IActionResult>Get()
        {
           var response = await _mediator.Send(new GetAllIngredientQry());

           var lstIngrdnt = _mapper.Map<List<IngredientDto>>(response.Payload);              

           return response.IsError ? BadRequest(response.Errors): Ok(lstIngrdnt);
        }

        // GET api/<IngredientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<IngredientController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IngredientDto ingredient)
        {
            var response = await _mediator.Send(new AddIngredientCmmnd { Calories = ingredient.Calories,
            Name = ingredient.Name.ToLower()});

            return response.IsError ? BadRequest(response.Errors): Ok(ingredient);
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
