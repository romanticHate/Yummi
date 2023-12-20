using MediatR;
using Yummi.Application.Enum;
using Yummi.Application.Models;
using Yummi.Application.Validators;
using Yummi.Core.Interfaces;

namespace Yummi.Application.CQRS.Recipe.Cmmnd.hndlr
{
    public class AddRecipeCmmndHndlr : IRequestHandler<AddRecipeCmmnd,
        OperationResponse<Core.Entities.Recipe>>
    {
        private readonly IUnitOfWork _uOw;
        public AddRecipeCmmndHndlr(IUnitOfWork uOw)
        {
            _uOw = uOw;
        }
        public async Task<OperationResponse<Core.Entities.Recipe>> Handle(AddRecipeCmmnd request,
            CancellationToken cancellationToken)
        {
            var response = new OperationResponse<Core.Entities.Recipe>();           
           
            try
            {                
                var recipe = new Core.Entities.Recipe
                {
                    Name = request.Name,
                    CookTime = request.CookTime,
                    Description = request.Description,
                    Course = request.Course,
                    Instructions = request.Instructions,
                    Cuisine = request.Cuisine,
                    PrepTime = request.PrepTime
                };

                var recipeVldtr = new RecipeVldtr();
                var validator = recipeVldtr.Validate(recipe);
                if (!validator.IsValid)
                {                                  
                    foreach (var error in validator.Errors)
                    {
                        response.Errors.Add(new Error { Message = error.ErrorMessage,
                            Code = ErrorCode.ValidationError });
                    }
                    response.IsError = true;
                    return response;
                }

                await _uOw.RecipeRepository.AddAsync(recipe);
                await _uOw.SaveAsync();
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Errors.Add(new Error { Message = ex.Message });
            }
           
            return response;
        }
    }
}
