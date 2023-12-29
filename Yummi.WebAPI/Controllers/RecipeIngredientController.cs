using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yummi.Application.CQRS.RecipeIngredient.Qry;
using Yummi.Core.DTOs;
using Yummi.Core.Entities;
using Yummi.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yummi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {              
        private readonly IUnitOfWork _uOw;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RecipeIngredientController(           
            IUnitOfWork uOw,
            IMapper mapper,
            IMediator mediator)
        {                      
            _uOw = uOw;
            _mapper = mapper;
            _mediator = mediator;


        }

        // GET: api/<RecipeController>
        [HttpGet("GetAllRecipeIngredients")]
        public async Task<IActionResult> GetAllRecipeIngredients(string recipeName)
        {           
            var response = await _mediator.Send(new GetAllRecipeIngredientQry { RecipeName = recipeName.ToLower() });
            //var lstRecipeIngredients = _mapper.Map<List<RecipeIngredientDto>>(response.Payload);

            return response.IsError ? BadRequest(response.Errors) : Ok(response.Payload);
        }

        //// GET: api/<RecipeIngredientController>
        //[HttpGet]
        //public async Task<ActionResult> GetAll()
        //{
        //    var response = await _uOw.RecipeIngredientRepository.GetAllAsync();
        //    var data = _mapper.Map<List<RecipeIngredientDto>>(response);
        //    return Ok(data);
        //}

        // GET api/<RecipeIngredientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RecipeIngredientController>
        [HttpPost]
        public  async Task<IActionResult> Add([FromBody] RecipeIngredientDto value)
        {
            var data = _mapper.Map<RecipeIngredient>(value);
            await _uOw.RecipeIngredientRepository.AddAsync(data);
            await _uOw.SaveAsync();
            return Ok(data);
        }

        // PUT api/<RecipeIngredientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipeIngredientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
