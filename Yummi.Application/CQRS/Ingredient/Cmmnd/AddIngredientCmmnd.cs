using MediatR;
using Yummi.Application.Models;

namespace Yummi.Application.CQRS.Ingredient.Cmmnd
{
    public class AddIngredientCmmnd:IRequest<OperationResponse<AddIngredientCmmnd>>
    {
        public string Name { get; set; } = null!;
        public string? Calories { get; set; }
    }
}
