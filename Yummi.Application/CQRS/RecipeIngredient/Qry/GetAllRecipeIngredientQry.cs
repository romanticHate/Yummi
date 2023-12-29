using MediatR;
using Yummi.Application.Models;

namespace Yummi.Application.CQRS.RecipeIngredient.Qry
{
    public class GetAllRecipeIngredientQry:IRequest<OperationResponse<IEnumerable<Core.DTOs.IngredientAmountDto>>>
    {
        public string RecipeName { get; set; }
    }
}
