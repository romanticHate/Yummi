using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yummi.Application.CQRS.Recipe.Cmmnd;
using Yummi.Application.CQRS.Recipe.Qry;
using Yummi.Core.DTOs;
using Yummi.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yummi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        //private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _uOw;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public RecipeController(ILogger<RecipeController> logger,
            //IRecipeRepository recipeRepository,
            IUnitOfWork uOw,
            IMapper mapper,
            IMediator mediator)
        {
            _logger = logger;
            //_recipeRepository = recipeRepository;
            _uOw = uOw;
            _mapper = mapper;
            _mediator = mediator;
        }
       
        // GET: api/<RecipeController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //return await _recipeRepository.GetAllAsync();
            var response = await _mediator.Send(new GetAllRecipeQry());
            var lstRecipe = _mapper.Map<List<RecipeDto>>(response.Payload);

            return response.IsError ? BadRequest(response.Errors):Ok(lstRecipe);
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // return Ok(await _uOw.RecipeRepository.GetByIdAsync(id));
            var response = await _mediator.Send(new GetByIdRecipeQry { Id = id });
            var recipe = _mapper.Map<RecipeDto>(response.Payload);

            return response.IsError ? BadRequest(response.Errors):Ok(recipe);
        }

        // POST api/<RecipeController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RecipeDto recipe)
        {
            //var recipeMpd = _mapper.Map<Recipe>(recipe);
            //await _uOw.RecipeRepository.AddAsync(resp);
            //await _uOw.SaveChangesAsync();

            //return Ok("Recipe Add: " + recipeDto.Name);
            
            var result = await _mediator.Send(new AddRecipeCmmnd
            {
                Name = recipe.Name.ToLower(),
                Description = recipe.Description.ToLower(),
                Instructions = recipe.Instructions.ToLower(),
                Cuisine = recipe.Cuisine.ToLower(),
                PrepTime = recipe.PrepTime,
                Course = recipe.Course.ToLower(),
                CookTime = recipe.CookTime
            });

            return result.IsError ? BadRequest(result.Errors): Ok();
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] RecipeDto recipe)
        {
            var result = await _mediator.Send(new UpdateRecipeCmmnd
            {
                Name = recipe.Name.ToLower(),
                Description = recipe.Description.ToLower(),
                Instructions = recipe.Instructions.ToLower(),
                Cuisine = recipe.Cuisine.ToLower(),
                PrepTime = recipe.PrepTime,
                Course = recipe.Course.ToLower(),
                CookTime = recipe.CookTime
            });

            return result.IsError ? BadRequest(result.Errors) : Ok();
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteRecipeCmmnd { Id = id });          
            return response.IsError ? BadRequest(response.Errors): Ok();
        }
    }
}
