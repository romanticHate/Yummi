using MediatR;
using Yummi.Application.Models;
using Yummi.Core.Interfaces;

namespace Yummi.Application.CQRS.RecipeIngredient.Qry.Hndlr
{
    public class GetAllRecipeIngredientHndlr:IRequestHandler<GetAllRecipeIngredientQry,
        OperationResponse<IEnumerable<Core.DTOs.IngredientAmountDto>>>
    {
        private readonly IUnitOfWork _uOw;
        public GetAllRecipeIngredientHndlr(IUnitOfWork uOw)
        {
            _uOw = uOw;
        }

        public async Task<OperationResponse<IEnumerable<Core.DTOs.IngredientAmountDto>>> Handle(GetAllRecipeIngredientQry request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResponse<IEnumerable<Core.DTOs.IngredientAmountDto>>();
            try
            {
                result.Payload = await _uOw.RecipeIngredientRepository.GetAllRecipeIngredients(request.RecipeName);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Errors.Add(new Error { Message = ex.Message });
            }

            return result;
        }
    }
}
