using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
       
        public RecipeIngredientController(           
            IUnitOfWork uOw,
            IMapper mapper)
        {                      
            _uOw = uOw;
            _mapper = mapper;
           
        }

        // GET: api/<RecipeController>
        [HttpGet("GetAllRecipeIngredients")]
        public async Task<IActionResult> GetAllRecipeIngredients(string recipeName)
        {
            var lstRcpIngrdnt = await _uOw.RecipeIngredientRepository.GetAllRecipeIngredients(recipeName.ToLower());
            return Ok(lstRcpIngrdnt);
            //var response = await _mediator.Send(new GetRecipeIngredientsQry());
            //var lstRecipeIngredients = _mapper.Map<List<RecipeIngredientsDto>>(response.Payload);

            //return response.IsError ? BadRequest(response.Errors) : Ok(lstRecipeIngredients);
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
