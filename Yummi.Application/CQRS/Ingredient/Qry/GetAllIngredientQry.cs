using MediatR;
using Yummi.Application.Models;

namespace Yummi.Application.CQRS.Ingredient.Qry
{
    public class GetAllIngredientQry : IRequest<OperationResponse<IEnumerable<Core.Entities.Ingredient>>>
    {
    }
}
