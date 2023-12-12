using MediatR;
using Yummi.Application.Models;

namespace Yummi.Application.CQRS.Recipe.Qry
{
    public class GetAllRecipeQry : IRequest<OperationResponse<IEnumerable<Core.Entities.Recipe>>>
    {

    }
}
