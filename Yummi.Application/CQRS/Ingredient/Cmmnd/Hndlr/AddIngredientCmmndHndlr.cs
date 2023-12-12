using MediatR;
using Yummi.Application.Enum;
using Yummi.Application.Models;
using Yummi.Application.Validators;
using Yummi.Core.Entities;
using Yummi.Core.Interfaces;

namespace Yummi.Application.CQRS.Ingredient.Cmmnd.Hndlr
{
    public class AddIngredientCmmndHndlr : IRequestHandler<AddIngredientCmmnd,
        OperationResponse<AddIngredientCmmnd>>
    {
        private readonly IUnitOfWork _uOw;
       
        public AddIngredientCmmndHndlr(IUnitOfWork uOw)
        {
            _uOw = uOw;
        }
        public async Task<OperationResponse<AddIngredientCmmnd>> Handle(AddIngredientCmmnd request,
            CancellationToken cancellationToken)
        {
            var response = new OperationResponse<AddIngredientCmmnd>();
            try
            {
                var ingredient = new Core.Entities.Ingredient
                {
                    Calories = request.Calories,
                    Name = request.Name
                };
                var ingredientVldtr = new IngredientVldtr();
                var validator = ingredientVldtr.Validate(ingredient);
                if (!validator.IsValid)
                {
                    foreach (var error in validator.Errors)
                    {
                        response.Errors.Add(new Error
                        {
                            Message = error.ErrorMessage,
                            Code = ErrorCode.ValidationError
                        });
                    }
                    response.IsError = true;
                    return response;
                }
                await _uOw.IngredientRepository.AddAsync(ingredient);
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
